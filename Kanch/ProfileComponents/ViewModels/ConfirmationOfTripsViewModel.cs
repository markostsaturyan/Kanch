﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows.Media.Imaging;
using IdentityModel.Client;
using Kanch.Commands;
using Kanch.DataModel;
using Kanch.ProfileComponents.DataModel;
using Kanch.ProfileComponents.HelperClasses;
using Kanch.ProfileComponents.Utilities;
using Newtonsoft.Json;

namespace Kanch.ProfileComponents.ViewModels
{
    public class ConfirmationOfTripsViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HttpClient httpClient;
        private TokenClient tokenClient;

        public ObservableCollection<UnconfirmedTrips> UnconfirmedTrips { get; set; }

        public ConfirmationOfTripsViewModel()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
            ConnectToServer();
            this.UnconfirmedTrips = new ObservableCollection<UnconfirmedTrips>();
            GetAllUnconfirmedTrips();
        }

        public void GetAllUnconfirmedTrips()
        {
            var tokenResponse = tokenClient.RequestRefreshTokenAsync(ConfigurationManager.AppSettings["refreshToken"]).Result;

            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("api/UserRegisteredTripsManagement").Result;

            var content = response.Content;

            var jsonContent = content.ReadAsStringAsync().Result;

            var trips = JsonConvert.DeserializeObject<List<CampingTrip>>(jsonContent);

            var campingTrips = new ObservableCollection<UnconfirmedTrips>();

            if (trips != null)
            {
                foreach (var trip in trips)
                {
                    var campingtrip = new CampingTripInfo()
                    {
                        ID = trip.ID,
                        Place = trip.Place,
                        DepartureDate = trip.DepartureDate,
                        ArrivalDate = trip.ArrivalDate,
                        CountOfMembers = trip.CountOfMembers,
                        MinAge = trip.MinAge,
                        MaxAge = trip.MaxAge,
                        Direction = trip.Direction,
                        HasGuide = trip.HasGuide,
                        HasPhotographer = trip.HasPhotographer
                    };
                    if (trip.TypeOfTrip == Kanch.DataModel.TypeOfCampingTrip.Campaign)
                    {
                        campingtrip.TypeOfTrip = ProfileComponents.DataModel.TypeOfCampingTrip.Campaign;
                    }
                    else if (trip.TypeOfTrip == Kanch.DataModel.TypeOfCampingTrip.CampingTrip)
                    {
                        campingtrip.TypeOfTrip = ProfileComponents.DataModel.TypeOfCampingTrip.CampingTrip;
                    }
                    else
                    {
                        campingtrip.TypeOfTrip = ProfileComponents.DataModel.TypeOfCampingTrip.Excursion;
                    }
                    if (trip.Food != null)
                    {
                        campingtrip.Food = new ObservableCollection<FoodInfo>();
                        foreach (var food in trip.Food)
                        {
                            campingtrip.Food.Add(new FoodInfo()
                            {
                                Name = food.Name,
                                Measure = food.Measure,
                                MeasurementUnit = food.MeasurementUnit
                            });
                        }
                    }
                    campingtrip.Organizer = new UserInfo()
                    {
                        FirstName = trip.Organizer.FirstName,
                        LastName = trip.Organizer.LastName,
                        UserName = trip.Organizer.UserName,
                        Email = trip.Organizer.Email,
                        PhoneNumber = trip.Organizer.PhoneNumber,
                        DateOfBirth = trip.Organizer.DateOfBirth,
                        Gender = trip.Organizer.Gender,
                        Image=ImageConverter.ConvertImageToImageSource(trip.Organizer.Image)??ImageConverter.DefaultProfilePicture(trip.Organizer.Gender)
                    };
                    

                    var unconfirmTrip = new HelperClasses.UnconfirmedTrips();

                    unconfirmTrip.CampingTrip = campingtrip;
                    unconfirmTrip.Trip = trip;
                    unconfirmTrip.ConfirmCommand = new Command(Confirm);
                    unconfirmTrip.IgnoreCommand = new Command(Ignore);

                    this.UnconfirmedTrips.Add(unconfirmTrip);
                }
            }
        }

        public void Confirm(object trip)
        {
            var tokenResponse = tokenClient.RequestRefreshTokenAsync(ConfigurationManager.AppSettings["refreshToken"]).Result;

            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var campingTrip = (trip as UnconfirmedTrips).Trip;
            var tripInfo = JsonConvert.SerializeObject(campingTrip);
            

            var response = httpClient.PutAsync("api/UserRegisteredTripsManagement/" + campingTrip.ID, new StringContent(tripInfo, Encoding.UTF8, "application/json")).Result;
            this.UnconfirmedTrips.Remove(trip as UnconfirmedTrips);
        }
        public void Ignore(object trip)
        {
            var tokenResponse = tokenClient.RequestRefreshTokenAsync(ConfigurationManager.AppSettings["refreshToken"]).Result;

            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var campingTrip = (trip as UnconfirmedTrips).Trip;
            var response = httpClient.DeleteAsync("api/UserRegisteredTripsManagement/" + campingTrip.ID).Result;

            this.UnconfirmedTrips.Remove(trip as UnconfirmedTrips);
        }
        private void ConnectToServer()
        {
            var disco = DiscoveryClient.GetAsync(ConfigurationManager.AppSettings["authenticationService"]).Result;

            if (disco.IsError)
            {
                return;
            }
            else
            {
                this.tokenClient = new TokenClient(disco.TokenEndpoint, "kanchDesktopApp", "secret");
            }
        }
    }
}

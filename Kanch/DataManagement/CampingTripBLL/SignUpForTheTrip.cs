﻿using System.Threading.Tasks;
using Kanch.DataManagement.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Kanch.DataManagement.Model.UsersDAL;
using Kanch.DataManagement.Model.Users;

namespace Kanch.DataManagement.CampingTripBLL
{
    public class SignUpForTheTrip : ISignUpForTheTrip
    {
        private readonly CampingTripContext campingTripContext;

        private readonly CampingTripRepository campingTripRepository;

        public SignUpForTheTrip(IOptions<Settings> settings)
        {
            campingTripContext = new CampingTripContext(settings);
            campingTripRepository = new CampingTripRepository(settings);
        }

        public async Task<UpdateResult> AsDriver(int id,string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update
                            .Set(s => s.DriverID, id);

            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async  Task<Driver> GetDriver(string id)
        {
            var trip = await campingTripContext.CampingTrips
                            .Find(Builders<CampingTrip>.Filter.Eq("Id", id))
                            .FirstOrDefaultAsync();
            var userDal = new UsersDal();
            return userDal.GetDriver(trip.DriverID);
        }

        public async Task<Guide> GetGuide(string id)
        {
            var trip = await campingTripContext.CampingTrips
                            .Find(Builders<CampingTrip>.Filter.Eq("Id", id))
                            .FirstOrDefaultAsync();
            var userDal = new UsersDal();
            return userDal.GetGuide(trip.GuideID);
        }

        public async Task<Photographer> GetPhotographer(string id)
        {
            var trip = await campingTripContext.CampingTrips
                            .Find(Builders<CampingTrip>.Filter.Eq("Id", id))
                            .FirstOrDefaultAsync();
            var userDal = new UsersDal();
            return userDal.GetPhotographer(trip.PhotographerID);
        }
        public async Task<UpdateResult> RemoveDriverFromTheTrip(string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update.Set(s => s.DriverID, 0);
            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> AsGuide(int id, string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update
                            .Set(s => s.GuideID, id);

            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> RemoveGuideFromTheTrip(string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update.Set(s => s.GuideID, 0);
            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> AsPhotographer(int id, string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update
                            .Set(s => s.PhotographerID, id);

            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async Task<UpdateResult> RemovePhotographerFromTheTrip(string campingTripID)
        {
            var filter = Builders<CampingTrip>.Filter.Eq(s => s.ID, campingTripID);
            var update = Builders<CampingTrip>.Update.Set(s => s.PhotographerID, 0);
            return await campingTripContext.CampingTrips.UpdateOneAsync(filter, update);
        }

        public async Task AsMember(int id, string campingTripID)
        {
            var campingTripFull = await campingTripRepository.GetCampingTrip(campingTripID);
            var campingTrip = new CampingTrip(campingTripFull);
            var userContext = new UserContext();
            var user = userContext.GetUser(id);
            if(campingTrip.MinAge <= user.Age && campingTrip.MaxAge >= user.Age)
            {
                userContext.SignUpForTheCamping(id, campingTripID);
                campingTrip.CountOfMembers++;
                if (campingTrip.CountOfMembers == campingTrip.MaxCountOfMembers)
                {
                    campingTrip.IsRegistrationCompleted = true;
                }

                await campingTripRepository.UpdateCampingTrip(campingTripID, campingTrip);

            }
        }

        public void RemoveMemberFromTheTrip(int id, string campingTripID)
        {
            var userDal = new UsersDal();
            userDal.RemoveMemberFromTheTrip(id, campingTripID);
        }
    }
}
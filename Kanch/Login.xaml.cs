﻿using IdentityModel;
using IdentityModel.Client;
using Kanch.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kanch
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private DiscoveryResponse disco;
        private TokenClient tokenClient;
        private Configuration config;

        public Login()
        {
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/Images/KanchLogo.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            ConnectToServerAsync();
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {


            var registration = new MainWindow();

            registration.Show();

            Application.Current.MainWindow = registration;

            this.Close();
        }

        private async void LoginClick(object sender, RoutedEventArgs e)
        {
            statusMessage.Visibility = Visibility.Hidden;

            if (userName.Text == "")
            {
                statusMessage.Text = "Please enter the user name";
                statusMessage.Visibility = Visibility.Visible;
                return;
            }

            if (password.Password=="")
            {
                statusMessage.Text = "Please enter the password";
                statusMessage.Visibility = Visibility.Visible;
                return;
            }

            // request token
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(userName.Text, password.Password, "campingTrip userManagement offline_access");

            if (tokenResponse.IsError)
            {
                if (tokenResponse.Error == "invalid_client")
                {
                    MessageBox.Show("You are not verified", "Reminder",MessageBoxButton.OK);

                    ConfigurationSettings.AppSettings["userName"] = userName.Text;

                    var verification = new Verification();

                    verification.Show();

                    this.Close();
                }

                statusMessage.Text = tokenResponse.ErrorDescription??tokenResponse.Error;
                statusMessage.Visibility = Visibility.Visible;
                return;
            }

            var handler = new JwtSecurityTokenHandler().ReadJwtToken(tokenResponse.AccessToken);

            var userId = handler.Claims.First(claim => claim.Type == "user_id");

            var role = handler.Claims.First(claim => claim.Type == JwtClaimTypes.Role);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["refreshToken"].Value = tokenResponse.RefreshToken;

            config.AppSettings.Settings["role"].Value = role.Value;

            config.AppSettings.Settings["userId"].Value = userId.Value;

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

            var profile = new Profile();

            profile.Show();

            Application.Current.MainWindow = profile;

            this.Close();
        }

        private void RetryConnectClick(object sender, RoutedEventArgs e)
        {
            ConnectToServerAsync();
        }

        private async void ConnectToServerAsync()
        {
            this.disco = DiscoveryClient.GetAsync(ConfigurationSettings.AppSettings["authenticationService"]).Result;

            if (disco.IsError)
            {
                statusMessage.Text = "Internet connection problems,\n please check your internet access";
                statusMessage.Visibility = Visibility.Visible;
                retryConnectTextBlock.Visibility = Visibility.Visible;
                password.IsEnabled = false;
                userName.IsEnabled = false;
                login.IsEnabled = false;
            }
            else
            {
                tokenClient = new TokenClient(disco.TokenEndpoint, "kanchDesktopApp", "secret");
                login.IsEnabled = true;
                retryConnectTextBlock.Visibility = Visibility.Hidden;
                password.IsEnabled = true;
                userName.IsEnabled = true;

                statusMessage.Text = "Connected";
                await Task.Run(() =>Thread.Sleep(3000));
                statusMessage.Visibility = Visibility.Hidden;

            }
        }

        private void ChangeVisibility(object sender, EventArgs e)
        {
            statusMessage.Visibility = Visibility.Hidden;
        }
    }
}

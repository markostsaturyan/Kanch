﻿using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Kanch.Commands;

namespace Kanch.ViewModel
{
    public class GuideViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string firstName;
        private string lastName;
        private string userName;
        private string email;
        private bool? male;
        private bool? female;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private string password;
        private string confirmPassword;
        private string profession;
        private string educationGrade;
        private string workExperience;
        private List<ListItem> languages;
        private ObservableCollection<string> places;
        private string inputPlace;

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                if (this.userName != value)
                {
                    this.userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        public string Email
        {
            get { return this.email; }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    NotifyPropertyChanged("Email");
                }
            }
        }

        public DateTime DateOfBirth
        {
            get { return this.dateOfBirth; }
            set
            {
                if (this.dateOfBirth != value)
                {
                    this.dateOfBirth = value;
                    NotifyPropertyChanged("DateOfBirth");
                }
            }
        }

        public bool? Male
        {
            get { return this.male; }
            set
            {
                this.male = value;
                NotifyPropertyChanged("Male");
            }
        }
        public bool? Female
        {
            get { return this.female; }
            set
            {
                this.female = value;
                NotifyPropertyChanged("Female");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                if (this.phoneNumber != value)
                {
                    this.phoneNumber = value;
                    NotifyPropertyChanged("PhoneNumber");
                }
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }
            set
            {
                if (this.confirmPassword != value)
                {
                    this.confirmPassword = value;
                    NotifyPropertyChanged("ConfirmPassword");
                }
            }
        }

        public string Profession
        {
            get
            {
                return this.profession;
            }
            set
            {
                if (this.profession != value)
                {
                    this.profession = value;
                    NotifyPropertyChanged("Profession");
                }
            }
        }

        public string EducationGrade
        {
            get
            {
                return this.educationGrade;
            }
            set
            {
                if (this.educationGrade != value)
                {
                    this.educationGrade = value;
                    NotifyPropertyChanged("EducationGrade");
                }
            }
        }

        public string WorkExperience
        {
            get
            {
                return this.workExperience;
            }
            set
            {
                if (this.workExperience != value)
                {
                    this.workExperience = value;
                    NotifyPropertyChanged("WorkExperience");
                }
            }
        }

        public List<ListItem> Languages
        {
            get { return this.languages; }
            set
            {
                if (this.languages != value)
                {
                    this.languages = value;
                    NotifyPropertyChanged("Languages");
                }
            }
        }

        public ObservableCollection<string> Places
        {
            get { return this.places; }
            set
            {
                this.places.Add(value.ToString());
                NotifyPropertyChanged("Places");
            }
        }

        public ICommand InputPlaceCommand { get; set; }

        public string InputPlace
        {
            get { return this.inputPlace; }
            set
            {
                if (this.inputPlace != value)
                {
                    this.inputPlace = value;
                    NotifyPropertyChanged("InputPlace");
                }
            }
        }

        public GuideViewModel()
        {
            Languages = new List<ListItem>();
            Languages.Add(new ListItem() { Text = "Armenian", IsSelected = false });
            Languages.Add(new ListItem() { Text = "Russian", IsSelected = false });
            Languages.Add(new ListItem() { Text = "English", IsSelected = false });
            Languages.Add(new ListItem() { Text = "German", IsSelected = false });
            Languages.Add(new ListItem() { Text = "Italian", IsSelected = false });
            Languages.Add(new ListItem() { Text = "French", IsSelected = false });
            this.places = new ObservableCollection<string>();
            this.InputPlaceCommand = new Command(AddPlace);
        }

        private void AddPlace(object obj)
        {
            if (InputPlace != null)
            {
                this.Places.Add(InputPlace);
                InputPlace = "";
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public void Reset()
        {
            this.Profession = "";
            this.EducationGrade = "";
            this.WorkExperience = "";
            foreach (var language in this.Languages)
            {
                language.IsSelected = false;
            }
        }

        private bool ProfessionValidation(ref string status)
        {
            if (Profession == null)
            {
                status = "Enter your profession.";
                return false;
            }
            return true;
        }

        private bool EducationGradeValidation(ref string status)
        {
            if (EducationGrade == null)
            {
                status = "Enter your education grade.";
                return false;
            }
            return true;
        }

        private bool WorkExperienceValidation(ref string status)
        {
            if (WorkExperience == null)
            {
                status = "Enter your work experience.";
                return false;
            }
            return true;
        }

        public bool GuideInfoValidation(out string status)
        {
            status = "";
            if (!ProfessionValidation(ref status))
                return false;
            else if (!EducationGradeValidation(ref status))
                return false;
            else if (!WorkExperienceValidation(ref status))
                return false;
            return true;
        }

        public class ListItem : INotifyPropertyChanged
        {
            private bool isSelected;
            public string Text { get; set; }

            public bool IsSelected
            {
                get { return this.isSelected; }
                set
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string strPropertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(strPropertyName));
            }
        }
    }
}

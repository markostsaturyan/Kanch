﻿namespace Kanch.DataModel
{
    public class Food
    {
        public string Name { get; set; }
        public string MeasurementUnit { get; set; }
        public double Measure { get; set; }
        public double Price { get; set; }
    }

    public enum TypeOfCampingTrip
    {
        Excursion,
        Campaign,
        CampingTrip
    }

    public enum TypeOfOrganization
    {
        OrderByUser,
        OrderByAdmin
    }
}

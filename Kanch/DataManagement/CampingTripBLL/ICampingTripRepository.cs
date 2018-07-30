﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kanch.DataManagement.Model;
using MongoDB.Driver;

namespace Kanch.DataManagement.CampingTripBLL
{
    public interface ICampingTripRepository
    {
        Task<IEnumerable<CampingTripFull>> GetAllRegistartionCompletedCampingTrips();
        Task<IEnumerable<CampingTripFull>> GetAllRegistartionNotCompletedCampingTrips();
        Task<IEnumerable<CampingTripFull>> GetAllCompletedCampingTrips();
        Task<IEnumerable<CampingTripFull>> GetCampingTrips();
        Task<CampingTripFull> GetCampingTrip(string id);
        Task AddCampingTrip(CampingTrip item);
        Task<DeleteResult> RemoveCampingTrip(string id);
        Task<ReplaceOneResult> UpdateCampingTrip(string id, CampingTrip trip);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampingTripService.DataManagement.Model;
using CampingTripService.DataManagement.CampingTripBLL;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CampingTripService.Controllers
{
    [Produces("application/json")]
    [Route("api/DriverTrips")]
    public class DriverTripsController : Controller
    {
        private readonly ICampingTripRepository campingTripRepository;

        public DriverTripsController(ICampingTripRepository campingTripRepository)
        {
            this.campingTripRepository = campingTripRepository;
        }

        // GET: api/DriverTrips
        [Authorize(Policy = "OnlyForDriver")]
        [HttpGet]
        public async Task<IEnumerable<CampingTripFull>> Get()
        {
            var identity = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity.Claims;

            var userIdClaim = claims.Where(claim => claim.Type == "user_id").First();

            if (userIdClaim == null) throw new Exception("user_id claim not found");

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new Exception("Invalid value for user_id in users claims");
            }

            return await campingTripRepository.GetDriverTripsAsync(userId);
        }

        // GET: api/DriverTrips/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IEnumerable<CampingTripFull>> Get(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;

            IEnumerable<Claim> claims = identity.Claims;

            var userIdClaims = claims.Where(claim => claim.Type == "user_id");

            if (userIdClaims != null)
            {
                var userIdClaim = userIdClaims.First();

                if (userIdClaim == null) throw new Exception("user_id claim not found");

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    throw new Exception("Invalid value for user_id in users claims");
                }

                var roleClaim = claims.Where(claim => claim.Type == "role")?.FirstOrDefault();

                if (userId != id && roleClaim?.Value!="Driver") return null;

                return await campingTripRepository.GetDriverTripsAsync(userId);
            }

            var clientIdClaims = claims.Where(claim => claim.Type == "client_id");

            if (clientIdClaims != null)
            {
                var clientIdClaim = clientIdClaims.First();

                if (clientIdClaim.Value == "userManagement")
                {
                    return await campingTripRepository.GetDriverTripsAsync(id);
                }
            }

            return null;
        }
        
    }
}

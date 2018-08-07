﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace CampingTripService.DataManagement.Model
{
    [DataContract]
    public class ServiceRequest
    {
        [BsonId]
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int ProviderId { get; set; }

        [DataMember]
        public string CampingTripId { get; set; }

        [DataMember]
        public DateTime RequestValidityPeriod { get; set; }
    }
}

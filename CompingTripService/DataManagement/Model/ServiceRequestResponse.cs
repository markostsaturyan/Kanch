﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace CampingTripService.DataManagement.Model
{
    [DataContract]
    public class ServiceRequestResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataMember]
        public int ProviderId { get; set; }

        [DataMember]
        public string CampingTripId { get; set; }

        [DataMember]
        public DateTime ResponseValidityPeriod { get; set; }

        [DataMember]
        public string ProviderRole { get; set; }

        [DataMember]
        public double Price { get; set; }
    }
}

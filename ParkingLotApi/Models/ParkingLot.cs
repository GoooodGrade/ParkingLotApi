﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkingLotApi.Models;

public class ParkingLot
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
}
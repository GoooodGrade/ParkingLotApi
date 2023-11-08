﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkingLotApi.Dtos;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repositories;

public class ParkingLotsRepository : IParkingLotsRepository
{
    private readonly IMongoCollection<ParkingLot> _dbCollection;
    public ParkingLotsRepository(IOptions<DataBaseSettings> dataBaseSettings)
    {
        var MongoClient = new MongoClient(dataBaseSettings.Value.ConnectionString);
        var MongoDataBase = MongoClient.GetDatabase(dataBaseSettings.Value.DataBaseName);
        _dbCollection = MongoDataBase.GetCollection<ParkingLot>(dataBaseSettings.Value.CollectionName);
    }

    public async Task<ParkingLot> CreateParkingLot(ParkingLot parkingLot)
    {

        await _dbCollection.InsertOneAsync(parkingLot);
        return await GetParkingLotByName(parkingLot.Name);
    }

    public async Task<ParkingLot> GetParkingLotByName(string parkingLotName)
    {
        return await _dbCollection.Find(doc=>doc.Name == parkingLotName).FirstOrDefaultAsync();
    }

    public async Task<List<ParkingLot>> GetAllParkingLots()
    {
        return await _dbCollection.Find(_ => true).ToListAsync();
    }
}
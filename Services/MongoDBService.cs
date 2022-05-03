using BabyMedsAzure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BabyMedsAzure.Services;

public class MongoDBService {

    private readonly IMongoCollection<Medicine> _babyMedsCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
		
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _babyMedsCollection = database.GetCollection<Medicine>(mongoDBSettings.Value.CollectionName);
    }

	public async Task AddMedicineAsync(Medicine medicine) {
		await _babyMedsCollection.InsertOneAsync(medicine);
    	return;
	}
	public async Task<List<Medicine>> GetAsync() {
		return await _babyMedsCollection.Find(new BsonDocument()).ToListAsync();
	}

	public void AddMedicineTest(string name) {
		var med = new Medicine();
		med.medicineName = name;
		_babyMedsCollection.InsertOneAsync(med);
		
	}

}
using BabyMedsAzure.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BabyMedsAzure.Services;

public class MongoDBService {

    private readonly IMongoCollection<Medicine> _babyMedsCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);

		Console.WriteLine("got client");
		
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

		Console.WriteLine("got database");

        _babyMedsCollection = database.GetCollection<Medicine>(mongoDBSettings.Value.CollectionName);

		Console.WriteLine("got collection");

		using (IAsyncCursor<BsonDocument> cursor = client.ListDatabases())
		{
			while (cursor.MoveNext())
			{
				foreach (var doc in cursor.Current)
				{
					Console.WriteLine(doc["name"]); // database name
				}
			}
		}
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
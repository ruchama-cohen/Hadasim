using MongoDB.Driver;

public class MongoService
{
    private readonly IMongoDatabase _database;

    public MongoService()
    {
        
        string connectionUri = "mongodb+srv://Hadasim2026:Test1234@cluster0.5tkryfl.mongodb.net/?appName=Cluster0\"";

        var client = new MongoClient(connectionUri);

        _database = client.GetDatabase("Hadasim");
    }
}

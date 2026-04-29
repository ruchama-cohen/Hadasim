using DL.Models;
using MongoDB.Driver;

public class TripDbContext
{
    private readonly IMongoDatabase _database;

    public TripDbContext()
    {
        var connectionString = "mongodb://Hadasim2026:Test1234@ac-6qryygn-shard-00-00.5tkryfl.mongodb.net:27017,ac-6qryygn-shard-00-01.5tkryfl.mongodb.net:27017,ac-6qryygn-shard-00-02.5tkryfl.mongodb.net:27017/?ssl=true&replicaSet=atlas-7fqs0r-shard-0&authSource=admin";
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("Hadasim");
    }

    public IMongoCollection<Teacher> Teachers => _database.GetCollection<Teacher>("Teachers");
    public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");
}
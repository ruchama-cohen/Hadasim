using DL.Models;
using MongoDB.Driver;

public class TripDbContext
{
    private readonly IMongoDatabase _database;

    public TripDbContext()
    {
        var connectionString = "mongodb+srv://Hadasim2026:Test1234@cluster0.5tkryfl.mongodb.net/?appName=Cluster0";
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("Hadasim");
    }

    public IMongoCollection<Teacher> Teachers => _database.GetCollection<Teacher>("Teachers");
    public IMongoCollection<Student> Students => _database.GetCollection<Student>("Students");
}
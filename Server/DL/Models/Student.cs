using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace DL.Models
{
    [BsonIgnoreExtraElements]
    public partial class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string SId { get; set; } = null!; 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        [BsonElement("Coordinates")]
        public CoordinateSystem Coordinates { get; set; }
        public DateTime LastUpdate { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DL.Models
{
    public partial class Teacher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? InternalId { get; set; } 

        [BsonElement("TeacherId")]
        public string TId { get; set; } = null!; 

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string ClassName { get; set; } = null!; 

    }
}

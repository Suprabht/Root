using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CMS.DomainModel.Contract;

namespace CMS.DomainModel.Model
{
    public class UserDetail : IUserDetail
    {
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("UserName")]
        public string UserName { get; set; }
        
        [BsonElement("ClientId")]
        public long ClientId { get; set; }
    }
}

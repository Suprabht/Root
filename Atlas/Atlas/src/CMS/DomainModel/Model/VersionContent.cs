using CMS.DomainModel.Contract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.DomainModel.Model
{
    public class VersionContent : IVersionContent
    {
        public ObjectId Id { get; set; }

        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; set; }

        [BsonElement("Text")]
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CMS.DomainModel.Contract;

namespace CMS.DomainModel.Model
{
    public class Content : IContent
    {
        public Content()
        {
        }

        public ObjectId Id { get; set; }

        [BsonElement("ContentId")]
        public Guid ContentId { get; set; }

        [BsonElement("ClientId")]
        public long ClientId { get; set; }

        [BsonElement("ContentName")]
        public string ContentName { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("MetaData")]
        public string MetaData { get; set; }

        [BsonElement("Text")]
        public string Text { get; set; }

        [BsonElement("Properties")]
        public Dictionary<string, string> Properties { get; set; }

        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; set; }

        [BsonElement("CreationUser")]
        public IUserDetail CreationUser { get; set; }

        [BsonElement("LastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        [BsonElement("LastModifiedUser")]
        public IUserDetail LastModifiedUser { get; set; }

        [BsonElement("ModifiedDatesTimes")]
        public DateTime[] ModifiedDatesTimes { get; set; }

        [BsonElement("TranslationDetailses")]
        public ITranslationDetails[] TranslationDetailses { get; set; }


    }
}

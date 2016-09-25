using CMS.DomainModel.Contract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.DomainModel.Model
{
    public class ImageContent : IImageContent
    {
        public ObjectId Id { get; set; }

        [BsonElement("ImageId")]
        public long ImageId { get; set; }   

        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("UplodedDate")]
        public DateTime UplodedDate { get; set; }

        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("ClientId")]
        public long ClientId { get; set; }

    }
}

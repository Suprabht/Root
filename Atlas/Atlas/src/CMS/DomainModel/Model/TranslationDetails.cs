using CMS.DomainModel.Contract;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.DomainModel.Model
{
    public class TranslationDetails : ITranslationDetails
    {
        [BsonElement("Language")]
        public string Language { get; set; }

        [BsonElement("ContentId")]
        public Guid ContentId { get; set; }
    }
}

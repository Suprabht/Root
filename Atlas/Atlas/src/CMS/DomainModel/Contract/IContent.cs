using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DomainModel.Model;
using MongoDB.Bson;

namespace CMS.DomainModel.Contract
{
    public interface IContent
    {
        ObjectId Id { get; set; }
        Guid ContentId { get; set; }
        long ClientId { get; set; }
        string ContentName { get; set; }
        string Title { get; set; }
        string MetaData { get; set; }
        string Text { get; set; }
        Dictionary<string, string> Properties { get; set; }
        DateTime CreationDate { get; set; }
        IUserDetail CreationUser { get; set; }
        DateTime LastModifiedDate { get; set; }
        IUserDetail LastModifiedUser { get; set; }
        DateTime[] ModifiedDatesTimes { get; set; }
        ITranslationDetails[] TranslationDetailses { get; set; }
    }
}

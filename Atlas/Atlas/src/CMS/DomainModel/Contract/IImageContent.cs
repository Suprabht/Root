using System;
using MongoDB.Bson;

namespace CMS.DomainModel.Contract
{
    public interface IImageContent
    {
        ObjectId Id { get; set; }
        long ImageId { get; set; }
        string ImageUrl { get; set; }
        DateTime UplodedDate { get; set; }
        bool IsDeleted { get; set; }
        long ClientId { get; set; }
    }
}
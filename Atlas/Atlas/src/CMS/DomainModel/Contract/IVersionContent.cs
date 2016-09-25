using System;
using MongoDB.Bson;

namespace CMS.DomainModel.Contract
{
    public interface IVersionContent
    {
        ObjectId Id { get; set; }
        DateTime CreationDate { get; set; }
        string Text { get; set; }
    }
}
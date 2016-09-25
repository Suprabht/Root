using MongoDB.Bson;

namespace CMS.DomainModel.Contract
{
    public interface IUserDetail
    {
        ObjectId Id { get; set; }
        string UserId { get; set; }
        string UserName { get; set; }
        long ClientId { get; set; }
    }
}
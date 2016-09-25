using System;

namespace CMS.DomainModel.Contract
{
    public interface ITranslationDetails
    {
        string Language { get; set; }
        Guid ContentId { get; set; }
    }
}
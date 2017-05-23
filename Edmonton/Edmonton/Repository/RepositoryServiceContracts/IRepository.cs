using DomainModel.ResponseContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.RepositoryServiceContracts
{
    public interface IRepository<T>
    {
        IResult Add(T element);
        IResult Add(List<T> elements);
        IResult Save(T element);
        IResult Save(List<T> elements);
        IResult Remove(T element);
        IResult Remove(List<T> elements);
        IResult FindBy(Int64 id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Repository.RepositoryServiceContracts;
using DomainModel.DataContracts;
using DomainModel.ResponseContracts;

namespace Repository.Dal
{
    public class UserRepository : IUserRepository<IUser>
    {
        public IResult Add(IUser element)
        {
            throw new NotImplementedException();
        }

        public IResult Add(List<IUser> elements)
        {
            throw new NotImplementedException();
        }

        public IResult FindBy(long id)
        {
            throw new NotImplementedException();
        }

        public IResult Remove(IUser element)
        {
            throw new NotImplementedException();
        }

        public IResult Remove(List<IUser> elements)
        {
            throw new NotImplementedException();
        }

        public IResult Save(IUser element)
        {
            throw new NotImplementedException();
        }

        public IResult Save(List<IUser> elements)
        {
            throw new NotImplementedException();
        }
    }
}

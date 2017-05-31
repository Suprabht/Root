using System;
using System.Collections.Generic;
using System.Text;
using Repository.RepositoryServiceContracts;
using DomainModel.DataContracts;
using DomainModel.ResponseContracts;
using Dal.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Dal
{
    public class UserRepository : IUserRepository<IUser>
    {
        private readonly BridgeToCareContext _context;

        public UserRepository(BridgeToCareContext context)
        {
            //DbContextOptions<BridgeToCareContext> option = new DbContextOptions<BridgeToCareContext>();
            //_context = new BridgeToCareContext(options.:connectionString);
            //DbContextOptionsBuilder options = new DbContextOptionsBuilder().UseSqlServer("connectionString");
            //_context = new DbContextOptions<BridgeToCareContext>();
            this._context = context;

        }

        //public UserRepository(BridgeToCareContext _context, )
        //{

        //}
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

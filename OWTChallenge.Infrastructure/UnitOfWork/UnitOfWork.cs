using OWTChallenge.Core.Interfaces;
using OWTChallenge.Infrastructure.Data;
using OWTChallenge.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWTChallenge.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private OWTChallengeContext context;
        public UnitOfWork(OWTChallengeContext context)
        {
            this.context = context;
            SkillRepo = new SkillRepository(this.context);
            ContactRepo = new ContactRepository(this.context);
        }
        public ISkillRepository SkillRepo
        {
            get;
            private set;
        }
        public IContactRepository ContactRepo
        {
            get;
            private set;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

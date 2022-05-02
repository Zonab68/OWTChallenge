using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWTChallenge.Core.Interfaces;

/// <summary>
/// And Unit of Work interface, 
/// which will be the connection layer between the WebAPI project and the repositories
/// </summary>
public interface IUnitOfWork : IDisposable
{
    ISkillRepository SkillRepo
    {
        get;
    }
    IContactRepository ContactRepo
    {
        get;
    }
    
    int Save();
}
using Nekretnine.Models;
using System.Collections.Generic;

namespace Nekretnine.Repository.Intefaces
{
    public interface IAgentRepository
    {
        IEnumerable<Agent> GetAll();
        Agent GetById(int id);
        IEnumerable<Agent> GetNajmladji();
        IEnumerable<Agent> GetEkstremi();
    }
}

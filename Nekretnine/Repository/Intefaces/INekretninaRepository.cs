using Nekretnine.Models;
using System.Collections.Generic;

namespace Nekretnine.Repository.Intefaces
{
    public interface INekretninaRepository
    {
        IEnumerable<Nekretnina> GetAll();
        Nekretnina GetById(int id);
        void Add(Nekretnina nekretnina);
        void Update(Nekretnina nekretnina);
        void Delete(Nekretnina nekretnina);
        IEnumerable<Nekretnina> GetByNapravljeno(int napravljeno);
        IEnumerable<Nekretnina> PostPretraga(decimal mini, decimal maksi);

    }
}

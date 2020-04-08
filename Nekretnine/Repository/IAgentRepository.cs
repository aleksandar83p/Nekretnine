using Nekretnine.Models;
using Nekretnine.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nekretnine.Repository
{
    public class AgentRepository : IDisposable, IAgentRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Agent> GetAll()
        {
            return db.Agenti;
        }

        public Agent GetById(int id)
        {
            return db.Agenti.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Agent> GetNajmladji()
        {
            return db.Agenti.OrderByDescending(x => x.GodinaRodjenja);
        }

        public IEnumerable<Agent> GetEkstremi()
        {

            List<Agent> Ekstremi = new List<Agent>();
            var min = db.Agenti.OrderBy(x => x.BrojProdatih).First();
            var max = db.Agenti.OrderByDescending(x => x.BrojProdatih).First();
            Ekstremi.Add(min);
            Ekstremi.Add(max);
            IEnumerable<Agent> rezultat = Ekstremi.AsEnumerable<Agent>();
            return rezultat.OrderByDescending(x => x.BrojProdatih);
        }
    }
}
using Nekretnine.Models;
using Nekretnine.Repository.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Nekretnine.Repository
{
    public class NekretninaRepository : IDisposable, INekretninaRepository
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

        public IEnumerable<Nekretnina> GetAll()
        {
            return db.Nekretnine.Include(x => x.Agent).OrderByDescending(x => x.Cena);
        }

        public Nekretnina GetById(int id)
        {
            return db.Nekretnine.Include(x => x.Agent).FirstOrDefault(x => x.Id == id);
        }

        public void Add(Nekretnina nekretnina)
        {
            db.Nekretnine.Add(nekretnina);
            db.SaveChanges();
        }

        public void Update(Nekretnina nekretnina)
        {
            db.Entry(nekretnina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Nekretnina nekretnina)
        {
            db.Nekretnine.Remove(nekretnina);
            db.SaveChanges();
        }

        public IEnumerable<Nekretnina> GetByNapravljeno(int napravljeno)
        {
            return db.Nekretnine.Include(x => x.Agent).Where(x => x.GodinaIzgradnje > napravljeno).OrderBy(x => x.GodinaIzgradnje);
        }

        public IEnumerable<Nekretnina> PostPretraga(decimal mini, decimal maksi)
        {
            return db.Nekretnine
                .Include(x => x.Agent)
                .Where(x => x.Kvadratura >= mini && x.Kvadratura < maksi)
                .OrderBy(x => x.Kvadratura);
        }
    }
}
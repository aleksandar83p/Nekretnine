namespace Nekretnine.Migrations
{
    using Nekretnine.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Nekretnine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Nekretnine.Models.ApplicationDbContext context)
        {
            context.Agenti.AddOrUpdate(x => x.Id,
            new Agent()
            {
                Id = 1,
                ImePrezime = "Pera Peric",
                Licenca = "Lic1",
                GodinaRodjenja = 1960,
                BrojProdatih = 15
            },
            new Agent()
            {
                Id = 2,
                ImePrezime = "Mika Mikic",
                Licenca = "Lic2",
                GodinaRodjenja = 1970,
                BrojProdatih = 10
            },
            new Agent()
            {
                Id = 3,
                ImePrezime = "Zika Zikic",
                Licenca = "Lic3",
                GodinaRodjenja = 1980,
                BrojProdatih = 5
            }
            );

            context.Nekretnine.AddOrUpdate(x => x.Id,
           new Nekretnina()
           {
               Id = 1,
               Mesto = "Novi Sad",
               AgencijskaOznaka = "Nek01",
               GodinaIzgradnje = 1974,
               Kvadratura = 50.00M,
               Cena = 40000.00M,
               AgentId = 1
           },
            new Nekretnina()
            {
                Id = 2,
                Mesto = "Beograd",
                AgencijskaOznaka = "Nek02",
                GodinaIzgradnje = 1990,
                Kvadratura = 60.00M,
                Cena = 50000.00M,
                AgentId = 2
            },
             new Nekretnina()
             {
                 Id = 3,
                 Mesto = "Subotica",
                 AgencijskaOznaka = "Nek03",
                 GodinaIzgradnje = 1995,
                 Kvadratura = 55.00M,
                 Cena = 45000.00M,
                 AgentId = 3
             },
              new Nekretnina()
              {
                  Id = 4,
                  Mesto = "Zrenjanin",
                  AgencijskaOznaka = "Nek04",
                  GodinaIzgradnje = 2010,
                  Kvadratura = 70.00M,
                  Cena = 60000.00M,
                  AgentId = 1
              }
              );
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nekretnine.Controllers;
using Nekretnine.Models;
using Nekretnine.Repository.Intefaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

namespace Nekretnine.Tests.Controllers
{
    [TestClass]
    public class NekretnineControllerTest
    {
        [TestMethod]
        public void GetReturns200AndObject()
        {
            // Arrange
            var mockRepository = new Mock<INekretninaRepository>();
            mockRepository.Setup(x => x.GetById(40)).Returns(new Nekretnina { Id = 40, Mesto = "Sombor", AgencijskaOznaka = "Aaa", GodinaIzgradnje = 2000, Kvadratura = 70.30M, Cena = 100.00M, AgentId = 1 });

            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(40);
            var contentResult = actionResult as OkNegotiatedContentResult<Nekretnina>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(40, contentResult.Content.Id);
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<INekretninaRepository>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(10);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<INekretninaRepository>();
            var controller = new NekretnineController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Nekretnina { Id = 9, Mesto = "Sombor", AgencijskaOznaka = "Aaa", GodinaIzgradnje = 2000, Kvadratura = 70.30M, Cena = 100.00M, AgentId = 1 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Nekretnina> nekretnine = new List<Nekretnina>
            {
                new Nekretnina { Id = 1, Mesto = "Sombor", AgencijskaOznaka = "Aaa", GodinaIzgradnje = 2000, Kvadratura = 70.30M, Cena = 100.00M, AgentId = 1 },
                new Nekretnina { Id = 2, Mesto = "Nis", AgencijskaOznaka = "Abba", GodinaIzgradnje = 2001, Kvadratura = 67.30M, Cena = 200.00M, AgentId = 2  }
            };
            var mockRepository = new Mock<INekretninaRepository>();
            mockRepository.Setup(x => x.PostPretraga(40, 80)).Returns(nekretnine.AsEnumerable());
            var controller = new NekretnineController(mockRepository.Object);

            Pretraga pretraga = new Pretraga() { Mini = 40, Maksi = 80 };

            // Act
            dynamic response = controller.PostPretraga(pretraga);
            var result = (IEnumerable<dynamic>)response.Content;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(nekretnine.Count, result.ToList().Count);
            Assert.AreEqual(nekretnine.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(nekretnine.ElementAt(1), result.ElementAt(1));

            Assert.AreEqual(response.GetType().GetGenericTypeDefinition(), typeof(OkNegotiatedContentResult<>));
        }

    }
}

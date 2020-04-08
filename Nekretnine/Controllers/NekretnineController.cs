using Nekretnine.Models;
using Nekretnine.Repository.Intefaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Nekretnine.Controllers
{
    public class NekretnineController : ApiController
    {
        private INekretninaRepository _repository { get; set; }

        public NekretnineController(INekretninaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Nekretnina> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }
            return Ok(nekretnina);
        }

        public IHttpActionResult Post(Nekretnina nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(nekretnina);
            return CreatedAtRoute("DefaultApi", new { id = nekretnina.Id }, nekretnina);
        }

        [Authorize]
        public IHttpActionResult Put(int id, Nekretnina nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nekretnina.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(nekretnina);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(nekretnina);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            _repository.Delete(nekretnina);
            return Ok();
        }

        public IEnumerable<Nekretnina> GetByNapravljeno(int napravljeno)
        {
            return _repository.GetByNapravljeno(napravljeno);
        }

        [Authorize]
        [Route("api/pretraga")]
        public IHttpActionResult PostPretraga(Pretraga pretraga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (pretraga.Mini > pretraga.Maksi)
            {
                return BadRequest("Mini ne sme biti vece od Maksi pretrage");
            }
            var nekretnine = _repository.PostPretraga(pretraga.Mini, pretraga.Maksi);
            if (nekretnine == null)
            {
                return NotFound();
            }
            return Ok(nekretnine);
        }

    }
}

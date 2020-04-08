using Nekretnine.Models;
using Nekretnine.Repository.Intefaces;
using System.Collections.Generic;
using System.Web.Http;

namespace Nekretnine.Controllers
{
    public class AgentiController : ApiController
    {
        private IAgentRepository _repository { get; set; }

        public AgentiController(IAgentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Agent> Get()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            var agent = _repository.GetById(id);
            if (agent == null)
            {
                return NotFound();
            }
            return Ok(agent);
        }

        [Route("api/najmladji")]
        public IEnumerable<Agent> GetNajmladji()
        {
            return _repository.GetNajmladji();
        }

        [Route("api/ekstremi")]
        public IEnumerable<Agent> GetEkstremi()
        {
            return _repository.GetEkstremi();
        }
    }
}

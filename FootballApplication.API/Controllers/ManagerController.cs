using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        protected ManagerRepository Repository {get;}

        public ManagerController(ManagerRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Manager> GetManager([FromRoute] int id)
        {
            Manager manager = Repository.GetManagerById(id);
            if (manager == null) {
                return NotFound();
            }

            return Ok(manager);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Manager>> GetManagers()
        {
            return Ok(Repository.GetManagers());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Manager manager) {
            if (manager == null)
            {
                return BadRequest("Manager info not correct");
            }

            bool status = Repository.InsertManager(manager);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateManager([FromBody] Manager manager)
        {
            if (manager == null)
            {
                return BadRequest("Manager info not correct");
            }

            Manager existingLeague = Repository.GetManagerById(manager.Id);
            if (existingLeague == null)
            {
                return NotFound($"Manager with id  not found");
            }

            bool status = Repository.UpdateManager(manager);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteManager([FromRoute] int id) {
            Manager existingLeague = Repository.GetManagerById(id);
            if (existingLeague == null)
            {
                return NotFound($"Manager with id {id} not found");
            }

            bool status = Repository.DeleteManager(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Manager with id {id}");        
        }
    }
}

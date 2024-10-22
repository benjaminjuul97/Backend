using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        protected StadiumRepository Repository {get;}

        public StadiumController(StadiumRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Stadium> GetStadium([FromRoute] int id)
        {
            Stadium stadiums = Repository.GetStadiumByID(id);
            if (stadiums == null) {
                return NotFound();
            }

            return Ok(stadiums);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Stadium>> GetStadiums()
        {
            return Ok(Repository.GetStadiums());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Stadium stadiums) {
            if (stadiums == null)
            {
                return BadRequest("Stadium info not correct");
            }

            bool status = Repository.InsertStadiums(stadiums);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateStadiums([FromBody] Stadium stadiums)
        {
            if (stadiums == null)
            {
                return BadRequest("Stadium info not correct");
            }

            Stadium existingStadium = Repository.GetStadiumByID(stadiums.Id);
            if (existingStadium == null)
            {
                return NotFound($"Stadium with id  not found");
            }

            bool status = Repository.UpdateStadium(stadiums);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStadium([FromRoute] int id) {
            Stadium existingStadium = Repository.GetStadiumByID(id);
            if (existingStadium == null)
            {
                return NotFound($"Stadium with id {id} not found");
            }

            bool status = Repository.DeleteStadium(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Stadium with id {id}");        
        }
    }
}

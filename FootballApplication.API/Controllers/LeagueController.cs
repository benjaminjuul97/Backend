using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        protected LeagueRepository Repository {get;}

        public LeagueController(LeagueRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<League> GetLeague([FromRoute] int id)
        {
            League league = Repository.GetLeagueByID(id);
            if (league == null) {
                return NotFound();
            }

            return Ok(league);
        }

        [HttpGet]
        public ActionResult<IEnumerable<League>> GetLeagues()
        {
            return Ok(Repository.GetLeagues());
        }

        [HttpPost]
        public ActionResult Post([FromBody] League league) {
            if (league == null)
            {
                return BadRequest("League info not correct");
            }

            bool status = Repository.InsertLeague(league);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateClub([FromBody] League league)
        {
            if (league == null)
            {
                return BadRequest("League info not correct");
            }

            League existingLeague = Repository.GetLeagueByID(league.Id);
            if (existingLeague == null)
            {
                return NotFound($"League with id  not found");
            }

            bool status = Repository.UpdateLeague(league);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLeague([FromRoute] int id) {
            League existingLeague = Repository.GetLeagueByID(id);
            if (existingLeague == null)
            {
                return NotFound($"League with id {id} not found");
            }

            bool status = Repository.DeleteLeague(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete League with id {id}");        
        }
    }
}

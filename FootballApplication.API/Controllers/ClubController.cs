using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        protected ClubRepository Repository {get;}

        public ClubController(ClubRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Club> GetClub([FromRoute] int id)
        {
            Club club = Repository.GetClubById(id);
            if (club == null) {
                return NotFound();
            }

            return Ok(club);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Club>> GetClubs()
        {
            return Ok(Repository.GetClubs());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Club club) {
            if (club == null)
            {
                return BadRequest("Club info not correct");
            }

            bool status = Repository.InsertClub(club);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateClub([FromBody] Club club)
        {
            if (club == null)
            {
                return BadRequest("Club info not correct");
            }

            Club existingClub = Repository.GetClubById(club.Id);
            if (existingClub == null)
            {
                return NotFound($"Club with id  not found");
            }

            bool status = Repository.UpdateClub(club);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteClub([FromRoute] int id) {
            Club existingClub = Repository.GetClubById(id);
            if (existingClub == null)
            {
                return NotFound($"Club with id {id} not found");
            }

            bool status = Repository.DeleteClub(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Club with id {id}");        
        }
    }
}

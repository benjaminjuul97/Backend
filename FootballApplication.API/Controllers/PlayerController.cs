// 
using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected PlayerRepository Repository {get;}

        public PlayerController(PlayerRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer([FromRoute] int id)
        {
            Player player = Repository.GetPlayerById(id);
            if (player == null) {
                return NotFound();
            }

            return Ok(player);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return Ok(Repository.GetPlayers());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Player player) {
            if (player == null)
            {
                return BadRequest("Player info not correct");
            }

            bool status = Repository.InsertPlayer(player);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdatePlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Player info not correct");
            }

            Player existingPlayer = Repository.GetPlayerById(player.Id);
            if (existingPlayer == null)
            {
                return NotFound($"Player with id  not found");
            }

            bool status = Repository.UpdatePlayer(player);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlayer([FromRoute] int id) {
            Player existingPlayer = Repository.GetPlayerById(id);
            if (existingPlayer == null)
            {
                return NotFound($"Player with id {id} not found");
            }

            bool status = Repository.DeletePlayer(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Player with id {id}");        
        }
    }
}

using FootballApplication.Model.Entities;
using FootballApplication.Model.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        protected TransferRepository Repository {get;}

        public TransferController(TransferRepository repository) {
            Repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Transfer> GetTransfer([FromRoute] int id)
        {
            Transfer transfers = Repository.GetTransferById(id);
            if (transfers == null) {
                return NotFound();
            }

            return Ok(transfers);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transfer>> GetTransfers()
        {
            return Ok(Repository.GetTransfers());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Transfer transfers) {
            if (transfers == null)
            {
                return BadRequest("Transfer info not correct");
            }

            bool status = Repository.InsertTransfer(transfers);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateTransfers([FromBody] Transfer transfers)
        {
            if (transfers == null)
            {
                return BadRequest("Transfer info not correct");
            }

            Transfer existingTransfer = Repository.GetTransferById(transfers.Id);
            if (existingTransfer == null)
            {
                return NotFound($"Transfer with id  not found");
            }

            bool status = Repository.UpdateTransfer(transfers);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTransfer([FromRoute] int id) {
            Transfer existingTransfer = Repository.GetTransferById(id);
            if (existingTransfer == null)
            {
                return NotFound($"Transfer with id {id} not found");
            }

            bool status = Repository.DeleteTransfer(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Unable to delete Transfer with id {id}");        
        }
    }
}

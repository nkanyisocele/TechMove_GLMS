using GLMS.API.Interfaces;
using GLMS.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GLMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepo;

        public ContractsController(IContractRepository contractRepo)
        {
            _contractRepo = contractRepo;
        }

        // GET: api/contracts
        [HttpGet]
        public async Task<IActionResult> GetContracts()
        {
            var contracts = await _contractRepo.GetAllContractsAsync(); // Match your method name
            return Ok(contracts); // Returns 200 OK with JSON
        }

        // POST: api/contracts
        [HttpPost]
        public async Task<IActionResult> CreateContract(Contract contract)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Returns 400 Bad Request

            await _contractRepo.AddContractAsync(contract);

            // Returns 201 Created with details
            return CreatedAtAction(nameof(GetContracts), new { id = contract.ContractId }, contract);
        }

        // PATCH: api/contracts/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var contract = await _contractRepo.GetContractByIdAsync(id);
            if (contract == null)
                return NotFound(new { Message = $"Contract {id} not found." }); // Returns 404 NotFound

            contract.Status = status;
            await _contractRepo.UpdateContractAsync(contract);

            return NoContent(); // Returns 204 No Content for a successful update
        }
    }
}


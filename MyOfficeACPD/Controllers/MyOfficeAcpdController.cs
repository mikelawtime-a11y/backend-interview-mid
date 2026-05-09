using Microsoft.AspNetCore.Mvc;
using MyOfficeACPD.Models;
using MyOfficeACPD.Repositories;

namespace MyOfficeACPD.Controllers
{
    [ApiController]
    [Route("api/myofficeacpd")]
    public class MyOfficeAcpdController : ControllerBase
    {
        private readonly IAcpdRepository _repository;

        public MyOfficeAcpdController(IAcpdRepository repository)
        {
            _repository = repository;
        }

        // GET /api/myofficeacpd
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET /api/myofficeacpd/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { message = $"Record with ID '{id}' not found." });
            return Ok(result);
        }

        // POST /api/myofficeacpd
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateAcpdRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Request body is required." });

            var created = await _repository.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.ACPD_SID }, created);
        }

        // PUT /api/myofficeacpd/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateAcpdRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Request body is required." });

            var updated = await _repository.UpdateAsync(id, request);
            if (!updated)
                return NotFound(new { message = $"Record with ID '{id}' not found." });

            var result = await _repository.GetByIdAsync(id);
            return Ok(result);
        }

        // DELETE /api/myofficeacpd/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Record with ID '{id}' not found." });

            return NoContent();
        }
    }
}

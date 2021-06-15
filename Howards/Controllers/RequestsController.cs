using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Howards.Models;

namespace Howards.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly DBContext _context;

        public RequestsController(DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Requests.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        /// <summary>
        /// Gets a specific Request.
        /// </summary>
        /// <param name="requestId"></param> 
        [HttpGet("{requestId}")]
        public async Task<ActionResult<Request>> GetRequest(Guid requestId)
        {
            var request = await _context.Request.FindAsync(requestId);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        /// <summary>
        /// Edits a Request.
        /// </summary>
        /// <remarks>
        /// Sample put:
        ///
        ///     PUT /Requests/{requestId}/{request}
        ///     {
        ///        "RequestId": a729ed69-512c-47ac-af17-212bc37a46a8,
        ///        "FirstName": "Brian",
        ///        "LastName": "Heredia",
        ///        "Id": "2421372300"
        ///        "Age": "25",
        ///        "House": "Hufflepuff"
        ///     }
        ///
        /// </remarks>
        /// <param name="requestId"></param>
        /// <param name="request"></param>
        /// <response code="202">If the data is valid and was saved.</response>
        /// <response code="400">If there is one or more validation errors.</response> 
        /// <response code="404">If a request with that id does not exists on db.</response> 
        [HttpPut("{requestId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutRequest(Guid requestId, Request request)
        {
            if (requestId != request.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(requestId))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }

            return Accepted();
        }

        /// <summary>
        /// Creates a new Request.
        /// </summary>
        /// <remarks>
        /// Sample post:
        ///
        ///     POST /Requests
        ///     {
        ///        "RequestId": a729ed69-512c-47ac-af17-212bc37a46a8,
        ///        "FirstName": "Brian",
        ///        "LastName": "Heredia",
        ///        "Id": "2421372300"
        ///        "Age": "25",
        ///        "House": "Hufflepuff"
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="202">If the data is valid and was saved.</response>
        /// <response code="400">If there is one or more validation errors.</response> 
        /// <response code="409">If a request with that id already exists on db.</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (RequestExists(request.RequestId))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest();
                }
            }

            return Accepted();
        }


        /// <summary>
        /// Deletes a specific Request.
        /// </summary>
        /// <param name="requestId"></param> 
        /// <response code="202">If the data was deleted correctly.</response>
        /// <response code="404">If a request with that id does not exists on db.</response> 
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequest(Guid requestId)
        {
            var request = await _context.Request.FindAsync(requestId);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        private bool RequestExists(Guid requestId)
        {
            return _context.Request.Any(e => e.RequestId == requestId);
        }
    }
}

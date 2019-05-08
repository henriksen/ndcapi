using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SessionsController : ControllerBase
	{
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
		/// <summary>
		/// Creates a new session
		/// </summary>
		/// <returns>The ID of the new session</returns>
		[HttpPost]
		public ActionResult<int> Post()
		{
            int sessionId = _sessionService.Create();
			return sessionId;
		}

		/// <summary>
		/// Updates a session with a new value
		/// </summary>
		/// <param name="id">Session ID</param>
		/// <param name="value">The new counter value</param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] int value)
		{
            _sessionService.Set(id, value);
		}

        /// <summary>
        /// Gets the current value of a session. 
        /// </summary>
        /// <param name="id">Session id</param>
        /// <returns>Current value.</returns>
        [HttpGet("{id}")]
        public ActionResult<int> Get(int id)
        {
            var session = _sessionService.Get(id);
            if (session is null) return NotFound();
            return session.Value;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarOfEvents_DataAccessLayer.Data;
using CalendarOfEvents_DataAccessLayer.Models;
using CalendarOfEvents_WebAPI.Infrastructure.DTO;

namespace CalendarOfEvents_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly CalendarOfEventsDbContext _context;
        public EventsController(CalendarOfEventsDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Event?>> GetTEventById(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEvent(Guid id, Event @event)
        {
            if(id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent([FromBody]PostEventDto eventDto)
        {
            //Al trabajr con DTOs se debe crear una variable del tipo Event que instacie un new Event
            //y los datos que no estan en el DTO se los genera
            Event @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = eventDto.Title,
                EventDate = eventDto.EventDate,
                Description = eventDto.Description,
                SendNotification = false
            };

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTEventById", new { id = @event.Id}, @event);
        }

        // DELETE: api/Events/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if(@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
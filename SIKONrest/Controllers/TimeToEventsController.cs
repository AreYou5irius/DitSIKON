using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SIKONrest;

namespace SIKONrest.Controllers
{
    public class TimeToEventsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/TimeToEvents
        public IQueryable<TimeToEvent> GetTimeToEvent()
        {
            return db.TimeToEvent;
        }

        // GET: api/TimeToEvents/5
        [ResponseType(typeof(TimeToEvent))]
        public IHttpActionResult GetTimeToEvent(int id)
        {
            TimeToEvent timeToEvent = db.TimeToEvent.Find(id);
            if (timeToEvent == null)
            {
                return NotFound();
            }

            return Ok(timeToEvent);
        }

        // PUT: api/TimeToEvents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeToEvent(int id, TimeToEvent timeToEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeToEvent.Event_ID)
            {
                return BadRequest();
            }

            db.Entry(timeToEvent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeToEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TimeToEvents
        [ResponseType(typeof(TimeToEvent))]
        public IHttpActionResult PostTimeToEvent(TimeToEvent timeToEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeToEvent.Add(timeToEvent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TimeToEventExists(timeToEvent.Event_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = timeToEvent.Event_ID }, timeToEvent);
        }

        // DELETE: api/TimeToEvents/5
        [ResponseType(typeof(TimeToEvent))]
        public IHttpActionResult DeleteTimeToEvent(int id)
        {
            TimeToEvent timeToEvent = db.TimeToEvent.Find(id);
            if (timeToEvent == null)
            {
                return NotFound();
            }

            db.TimeToEvent.Remove(timeToEvent);
            db.SaveChanges();

            return Ok(timeToEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeToEventExists(int id)
        {
            return db.TimeToEvent.Count(e => e.Event_ID == id) > 0;
        }
    }
}
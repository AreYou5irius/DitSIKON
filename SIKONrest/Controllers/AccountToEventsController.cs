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
    public class AccountToEventsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/AccountToEvents
        public IQueryable<AccountToEvent> GetAccountToEvent()
        {
            return db.AccountToEvent;
        }

        // GET: api/AccountToEvents/5
        [ResponseType(typeof(AccountToEvent))]
        public IHttpActionResult GetAccountToEvent(int id)
        {
            AccountToEvent accountToEvent = db.AccountToEvent.Find(id);
            if (accountToEvent == null)
            {
                return NotFound();
            }

            return Ok(accountToEvent);
        }

        // PUT: api/AccountToEvents/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccountToEvent(int id, AccountToEvent accountToEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountToEvent.Event_ID)
            {
                return BadRequest();
            }

            db.Entry(accountToEvent).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountToEventExists(id))
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

        // POST: api/AccountToEvents
        [ResponseType(typeof(AccountToEvent))]
        public IHttpActionResult PostAccountToEvent(AccountToEvent accountToEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountToEvent.Add(accountToEvent);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AccountToEventExists(accountToEvent.Event_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = accountToEvent.Event_ID }, accountToEvent);
        }

        // DELETE: api/AccountToEvents/5
        [ResponseType(typeof(AccountToEvent))]
        public IHttpActionResult DeleteAccountToEvent(int id)
        {
            AccountToEvent accountToEvent = db.AccountToEvent.Find(id);
            if (accountToEvent == null)
            {
                return NotFound();
            }

            db.AccountToEvent.Remove(accountToEvent);
            db.SaveChanges();

            return Ok(accountToEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountToEventExists(int id)
        {
            return db.AccountToEvent.Count(e => e.Event_ID == id) > 0;
        }
    }
}
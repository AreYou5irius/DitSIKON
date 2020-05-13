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
    public class AutistsController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Autists
        public IQueryable<Autist> GetAutist()
        {
            return db.Autist;
        }

        // GET: api/Autists/5
        [ResponseType(typeof(Autist))]
        public IHttpActionResult GetAutist(int id)
        {
            Autist autist = db.Autist.Find(id);
            if (autist == null)
            {
                return NotFound();
            }

            return Ok(autist);
        }

        // PUT: api/Autists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutist(int id, Autist autist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autist.ID)
            {
                return BadRequest();
            }

            db.Entry(autist).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutistExists(id))
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

        // POST: api/Autists
        [ResponseType(typeof(Autist))]
        public IHttpActionResult PostAutist(Autist autist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autist.Add(autist);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = autist.ID }, autist);
        }

        // DELETE: api/Autists/5
        [ResponseType(typeof(Autist))]
        public IHttpActionResult DeleteAutist(int id)
        {
            Autist autist = db.Autist.Find(id);
            if (autist == null)
            {
                return NotFound();
            }

            db.Autist.Remove(autist);
            db.SaveChanges();

            return Ok(autist);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutistExists(int id)
        {
            return db.Autist.Count(e => e.ID == id) > 0;
        }
    }
}
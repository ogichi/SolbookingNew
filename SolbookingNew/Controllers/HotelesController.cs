using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolbookingNew.Models;

namespace SolbookingNew.Controllers
{
    public class HotelesController : Controller
    {
        private HotelesEntities db = new HotelesEntities();

        // GET: Hoteles
        public ActionResult Index()
        {
            return View(db.Hoteles.ToList());
        }

        // GET: Hoteles/Details/5 son las habitaciones
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Encuentra el hotel
            Hotele hotele = db.Hoteles.Find(id);
            //Busca las habitaciones
            var habitacion = db.Habitaciones.Where(x => x.HotelId == id);

            if (habitacion == null)
            {
                return HttpNotFound();
            }
            
            return View(habitacion.ToList<Habitacione>());
        }

        // GET: Hoteles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hoteles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CIF,Nombre,Direccion,Ciudad,FechaAlta")] Hotele hotele)
        {
            if (ModelState.IsValid)
            {
                // Se inserta en BD la fecha de creación
                hotele.FechaAlta = @DateTime.Now;
                db.Hoteles.Add(hotele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotele);
        }

        // GET: Hoteles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotele hotele = db.Hoteles.Find(id);
            if (hotele == null)
            {
                return HttpNotFound();
            }
            return View(hotele);
        }

        // POST: Hoteles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hotele hotele)
        {
            if (ModelState.IsValid)
            {
                var hotel = db.Hoteles.Find(hotele.Id);
                hotel.CIF = hotele.CIF;
                hotel.Nombre = hotele.Nombre;
                hotel.Direccion = hotele.Direccion;
                hotel.Ciudad = hotele.Ciudad;
                // Al editar, añade la fecha de edición
                hotel.FechaModificacion = @DateTime.Now;                
                //db.Entry(hotele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotele);
        }

        // GET: Hoteles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotele hotele = db.Hoteles.Find(id);
            if (hotele == null)
            {
                return HttpNotFound();
            }
            return View(hotele);
        }

        // POST: Hoteles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Encuentra el hotel para eliminarlo
            Hotele hotele = db.Hoteles.Find(id);
            //Puede tener clave foranea, si es así, almacena los registros
            var habitacion = db.Habitaciones.Where(x => x.HotelId == id);
            //Si tiene habitaciones, las borra primero, si no, borra el hotel
            if (habitacion != null)
            {
                foreach (var item in habitacion)
                {
                    db.Habitaciones.Remove(item);
                }                
                db.Hoteles.Remove(hotele);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

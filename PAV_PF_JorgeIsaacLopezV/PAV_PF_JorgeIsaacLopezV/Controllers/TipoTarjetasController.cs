using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PAV_PF_JorgeIsaacLopezV;

namespace PAV_PF_JorgeIsaacLopezV.Controllers
{
    public class TipoTarjetasController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        // GET: TipoTarjetas
        public ActionResult Index()
        {
            return View(db.TipoTarjeta.ToList());
        }

        // GET: TipoTarjetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TipoTarjeta.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTarjetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_tarjeta,nombre_tarjeta")] TipoTarjeta tipoTarjeta)
        {
            if (ModelState.IsValid)
            {
                db.TipoTarjeta.Add(tipoTarjeta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TipoTarjeta.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // POST: TipoTarjetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_tarjeta,nombre_tarjeta")] TipoTarjeta tipoTarjeta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoTarjeta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoTarjeta);
        }

        // GET: TipoTarjetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTarjeta tipoTarjeta = db.TipoTarjeta.Find(id);
            if (tipoTarjeta == null)
            {
                return HttpNotFound();
            }
            return View(tipoTarjeta);
        }

        // POST: TipoTarjetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoTarjeta tipoTarjeta = db.TipoTarjeta.Find(id);
            db.TipoTarjeta.Remove(tipoTarjeta);
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

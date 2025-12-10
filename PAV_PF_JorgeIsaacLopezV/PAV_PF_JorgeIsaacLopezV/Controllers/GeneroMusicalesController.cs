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
    public class GeneroMusicalesController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        // GET: GeneroMusicales
        public ActionResult Index()
        {
            return View(db.GeneroMusical.ToList());
        }

        // GET: GeneroMusicales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusical.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // GET: GeneroMusicales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneroMusicales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_genero,codigo_genero,descripcion")] GeneroMusical generoMusical)
        {
            if (ModelState.IsValid)
            {
                db.GeneroMusical.Add(generoMusical);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generoMusical);
        }

        // GET: GeneroMusicales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusical.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // POST: GeneroMusicales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_genero,codigo_genero,descripcion")] GeneroMusical generoMusical)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generoMusical).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generoMusical);
        }

        // GET: GeneroMusicales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneroMusical generoMusical = db.GeneroMusical.Find(id);
            if (generoMusical == null)
            {
                return HttpNotFound();
            }
            return View(generoMusical);
        }

        // POST: GeneroMusicales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneroMusical generoMusical = db.GeneroMusical.Find(id);
            db.GeneroMusical.Remove(generoMusical);
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

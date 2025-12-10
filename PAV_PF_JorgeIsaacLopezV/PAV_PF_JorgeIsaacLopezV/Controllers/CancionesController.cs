using PAV_PF_JorgeIsaacLopezV;
using PAV_PF_JorgeIsaacLopezV.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PAV_PF_JorgeIsaacLopezV.Controllers
{
    public class CancionesController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        // GET: Canciones
        public ActionResult Index()
        {


            //ViewBag.id_genero = new SelectList(db.GeneroMusical, "id_genero", "descripcion");
            var canciones = db.Cancion.Include(c => c.GeneroMusical);
            return View(canciones.ToList());
        }

        // GET: Canciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            return View(cancion);
        }

        // GET: Canciones/Create
        public ActionResult Create()
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.id_genero = new SelectList(db.GeneroMusical, "id_genero", "descripcion");
            return View();
        }

        // POST: Canciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_cancion,codigo_cancion,id_genero,artista,nombre_cancion,precio")] Cancion cancion)
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Cancion.Add(cancion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_genero = new SelectList(db.GeneroMusical, "id_genero", "descripcion", cancion.id_genero);
            return View(cancion);
        }

        // GET: Canciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_genero = new SelectList(db.GeneroMusical, "id_genero", "descripcion", cancion.id_genero);
            return View(cancion);
        }

        // POST: Canciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_cancion,codigo_cancion,id_genero,artista,nombre_cancion,precio")] Cancion cancion)
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(cancion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_genero = new SelectList(db.GeneroMusical, "id_genero", "descripcion", cancion.id_genero);
            return View(cancion);
        }

        // GET: Canciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            return View(cancion);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!EsAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            Cancion cancion = db.Cancion.Find(id);
            db.Cancion.Remove(cancion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool EsAdmin()
        {
            var perfil = Session["PerfilNombre"] as string;
            return string.Equals(perfil, "Administrador", StringComparison.OrdinalIgnoreCase);
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

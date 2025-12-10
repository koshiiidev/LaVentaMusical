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
    public class UsuariosController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.Perfil).Include(u => u.TipoTarjeta);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.id_perfil = new SelectList(db.Perfil, "id_perfil", "nombre_perfil");
            ViewBag.id_tipo_tarjeta = new SelectList(db.TipoTarjeta, "id_tipo_tarjeta", "nombre_tarjeta");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,numero_identificacion,nombre_completo,genero,correo,id_tipo_tarjeta,numero_tarjeta,contrasena,id_perfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_perfil = new SelectList(db.Perfil, "id_perfil", "nombre_perfil", usuario.id_perfil);
            ViewBag.id_tipo_tarjeta = new SelectList(db.TipoTarjeta, "id_tipo_tarjeta", "nombre_tarjeta", usuario.id_tipo_tarjeta);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_perfil = new SelectList(db.Perfil, "id_perfil", "nombre_perfil", usuario.id_perfil);
            ViewBag.id_tipo_tarjeta = new SelectList(db.TipoTarjeta, "id_tipo_tarjeta", "nombre_tarjeta", usuario.id_tipo_tarjeta);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,numero_identificacion,nombre_completo,genero,correo,id_tipo_tarjeta,numero_tarjeta,contrasena,id_perfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_perfil = new SelectList(db.Perfil, "id_perfil", "nombre_perfil", usuario.id_perfil);
            ViewBag.id_tipo_tarjeta = new SelectList(db.TipoTarjeta, "id_tipo_tarjeta", "nombre_tarjeta", usuario.id_tipo_tarjeta);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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

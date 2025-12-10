using PAV_PF_JorgeIsaacLopezV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PAV_PF_JorgeIsaacLopezV.Controllers
{
    public class CuentaController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = db.Usuario
                            .Include("Perfil")
                            .FirstOrDefault(u =>
                                u.correo == model.Correo &&
                                u.contrasena == model.Contrasena);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                return View(model);
            }

            
            bool esAdminCorreo = string.Equals(
                usuario.correo,
                "admin@gmail.com",
                StringComparison.OrdinalIgnoreCase
            );

            if (esAdminCorreo)
            {
                
                var perfilAdmin = db.Perfil.FirstOrDefault(p => p.nombre_perfil == "Administrador");

                Session["UsuarioId"] = usuario.id_usuario;
                Session["NombreUsuario"] = usuario.nombre_completo;

                if (perfilAdmin != null)
                {
                    Session["PerfilId"] = perfilAdmin.id_perfil;
                    Session["PerfilNombre"] = perfilAdmin.nombre_perfil;
                }
                else
                {
                    
                    Session["PerfilId"] = usuario.id_perfil;
                    Session["PerfilNombre"] = "Administrador";
                }
            }
            else
            {
                
                Session["UsuarioId"] = usuario.id_usuario;
                Session["NombreUsuario"] = usuario.nombre_completo;
                Session["PerfilId"] = usuario.id_perfil;
                Session["PerfilNombre"] = usuario.Perfil != null ? usuario.Perfil.nombre_perfil : "";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.GeneroLista = new SelectList(
                new[]
                {
            new { Value = "Masculino", Text = "Masculino" },
            new { Value = "Femenino", Text = "Femenino" }
                },
                "Value",
                "Text"
            );

            ViewBag.IdTipoTarjeta = new SelectList(db.TipoTarjeta.ToList(), "id_tipo_tarjeta", "nombre_tarjeta");

            return View(new RegisterViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GeneroLista = new SelectList(
                    new[]
                    {
                new { Value = "Masculino", Text = "Masculino" },
                new { Value = "Femenino", Text = "Femenino" }
                    },
                    "Value",
                    "Text",
                    model.Genero
                );

                ViewBag.IdTipoTarjeta = new SelectList(db.TipoTarjeta, "IdTipoTarjeta", "NombreTarjeta", model.IdTipoTarjeta);

                return View(model);
            }

            
            if (db.Usuario.Any(u => u.correo == model.Correo))
            {
                ModelState.AddModelError("", "Ya existe un usuario registrado con ese correo.");

                ViewBag.GeneroLista = new SelectList(
                    new[]
                    {
                new { Value = "Masculino", Text = "Masculino" },
                new { Value = "Femenino", Text = "Femenino" }
                    },
                    "Value",
                    "Text",
                    model.Genero
                );

                ViewBag.IdTipoTarjeta = new SelectList(db.TipoTarjeta, "IdTipoTarjeta", "NombreTarjeta", model.IdTipoTarjeta);

                return View(model);
            }

            var perfilUsuario = db.Perfil.FirstOrDefault(p => p.nombre_perfil == "Usuario");
            if (perfilUsuario == null)
            {
                ModelState.AddModelError("", "No se encontró el perfil 'Usuario' en la base de datos.");
                return View(model);
            }

            var nuevoUsuario = new Usuario
            {
                numero_identificacion = model.NumeroIdentificacion,
                nombre_completo = model.NombreCompleto,
                genero = model.Genero,
                correo = model.Correo,
                id_tipo_tarjeta = model.IdTipoTarjeta,
                numero_tarjeta = model.NumeroTarjeta,
                contrasena = model.Contrasena,
                id_perfil = perfilUsuario.id_perfil
            };

            db.Usuario.Add(nuevoUsuario);
            db.SaveChanges();

           
            Session["UsuarioId"] = nuevoUsuario.id_usuario;
            Session["NombreUsuario"] = nuevoUsuario.nombre_completo;
            Session["PerfilId"] = nuevoUsuario.id_perfil;
            Session["PerfilNombre"] = perfilUsuario.nombre_perfil;

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
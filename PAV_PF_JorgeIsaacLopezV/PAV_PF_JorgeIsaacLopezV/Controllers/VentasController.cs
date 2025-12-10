using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PAV_PF_JorgeIsaacLopezV;
using PAV_PF_JorgeIsaacLopezV.Models.ViewModels;

namespace PAV_PF_JorgeIsaacLopezV.Controllers
{
    public class VentasController : Controller
    {
        private LaVentaMusicalEntities db = new LaVentaMusicalEntities();

        // GET: Ventas
        public ActionResult Index()
        {
            var venta = db.Venta.Include(v => v.Usuario);
            return View(venta.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "numero_identificacion");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_venta,numero_factura,fecha_compra,subtotal,iva,total,id_usuario")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "numero_identificacion", venta.id_usuario);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "numero_identificacion", venta.id_usuario);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_venta,numero_factura,fecha_compra,subtotal,iva,total,id_usuario")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_usuario = new SelectList(db.Usuario, "id_usuario", "numero_identificacion", venta.id_usuario);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Venta.Find(id);
            db.Venta.Remove(venta);
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

        private CarritoViewModel ObtenerCarrito()
        {
            var carrito = Session["Carrito"] as CarritoViewModel;
            if (carrito == null)
            {
                carrito = new CarritoViewModel();
                Session["Carrito"] = carrito;
            }
            return carrito;
        }

        public ActionResult AgregarAlCarrito(int idCancion)
        {
            var cancion = db.Cancion.Find(idCancion);
            if (cancion == null)
            {
                return HttpNotFound();
            }

            var carrito = ObtenerCarrito();

            
            var itemExistente = carrito.Items.FirstOrDefault(i => i.IdCancion == idCancion);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += 1;
            }
            else
            {
                carrito.Items.Add(new CarritoItemVM
                {
                    IdCancion = cancion.id_cancion,
                    NombreCancion = cancion.nombre_cancion,
                    Precio = cancion.precio,
                    Cantidad = 1
                });
            }

            Session["Carrito"] = carrito;

            
            return RedirectToAction("Index", "Canciones");
        }

        public ActionResult VerCarrito()
        {
            var carrito = ObtenerCarrito();
            if (Session["UsuarioId"] != null)
            {
                int idUsuario = (int)Session["UsuarioId"];

                
                var usuario = db.Usuario
                                .Include("TipoTarjeta")
                                .FirstOrDefault(u => u.id_usuario == idUsuario);

                if (usuario != null && !string.IsNullOrWhiteSpace(usuario.numero_tarjeta))
                {
                    var descripcionTarjeta = usuario.TipoTarjeta != null
                        ? usuario.TipoTarjeta.nombre_tarjeta
                        : "Tarjeta";

                    var ultimos4 = usuario.numero_tarjeta.Length >= 4
                        ? usuario.numero_tarjeta.Substring(usuario.numero_tarjeta.Length - 4)
                        : usuario.numero_tarjeta;

                    
                    var listaTarjetas = new[]
                    {
                new
                {
                    Id = usuario.id_tipo_tarjeta,
                    Descripcion = string.Format("{0} - ****{1}", descripcionTarjeta, ultimos4)
                }
            };

                    ViewBag.Tarjetas = new SelectList(listaTarjetas, "Id", "Descripcion", usuario.id_tipo_tarjeta);
                }
            }

            return View(carrito);
        }

        public ActionResult EliminarDelCarrito(int idCancion)
        {
            var carrito = ObtenerCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.IdCancion == idCancion);
            if (item != null)
            {
                carrito.Items.Remove(item);
                Session["Carrito"] = carrito;
            }

            return RedirectToAction("VerCarrito");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarCompra()
        {
            var carrito = ObtenerCarrito();

            if (carrito.Items == null || !carrito.Items.Any())
            {
                TempData["ErrorPago"] = "El carrito está vacío. No hay nada que comprar.";
                return RedirectToAction("VerCarrito");
            }

            if (Session["UsuarioId"] == null)
            {
                TempData["ErrorPago"] = "Debe iniciar sesión para realizar la compra.";
                return RedirectToAction("VerCarrito");
            }

            int idUsuario = (int)Session["UsuarioId"];

            var usuario = db.Usuario
                            .Include("TipoTarjeta")
                            .FirstOrDefault(u => u.id_usuario == idUsuario);

            if (usuario == null)
            {
                TempData["ErrorPago"] = "No se encontró la información del usuario.";
                return RedirectToAction("VerCarrito");
            }

            if (string.IsNullOrWhiteSpace(usuario.numero_tarjeta))
            {
                TempData["ErrorPago"] = "No tiene una tarjeta registrada. Actualice sus datos antes de realizar el pago.";
                return RedirectToAction("VerCarrito");
            }

            
            decimal subtotal = carrito.Subtotal;
            decimal iva = carrito.Iva;
            decimal total = carrito.Total;

            
            string numeroFactura = "FAC-" + DateTime.Now.Ticks;

            
            var venta = new Venta
            {
                numero_factura = numeroFactura,
                fecha_compra = DateTime.Now,
                subtotal = subtotal,
                iva = iva,
                total = total,
                id_usuario = idUsuario
            };

            db.Venta.Add(venta);
            db.SaveChanges(); 

            
            foreach (var item in carrito.Items)
            {
                var detalle = new DetalleVenta
                {
                    id_venta = venta.id_venta,
                    id_cancion = item.IdCancion,
                    cantidad = item.Cantidad,
                    precio_unitario = item.Precio,
                    subtotal = item.Subtotal
                };

                db.DetalleVenta.Add(detalle);
            }

            db.SaveChanges();

            
            Session["Carrito"] = null;

            
            TempData["NumeroFactura"] = venta.numero_factura;
            TempData["Total"] = venta.total;

            return RedirectToAction("CompraExitosa");
        }

        public ActionResult CompraExitosa()
        {
            return View();
        }

    }
}

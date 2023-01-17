using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DBPracticaConLoginSearchYList;
using DBPracticaConLogin;


namespace DBPracticaConLoginSearchYList.Controllers
{
    public class ClientesController : Controller
    {
        private FacturacionProdGrupooEntities db = new FacturacionProdGrupooEntities();

        // GET: Clientes
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {
            //var clientes = db.Clientes.Include(c => c.MetodoPago);
            return View(db.Clientes.Where(p => Criterio == null || p.Cedula.StartsWith(Criterio) ||
             p.Email.ToString().StartsWith(Criterio)
             || p.Telefono.ToString().StartsWith(Criterio)
             || p.MetodoPago.Descripcion.StartsWith(Criterio)
             || p.Nombre_Comercial.StartsWith(Criterio)).ToList());
        }

        // GET: Clientes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.MetodoPagoID = new SelectList(db.MetodoPago, "MetodoPagoID", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nombre_Comercial,RNC,Cedula,Email,Telefono,Activo,MetodoPagoID")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MetodoPagoID = new SelectList(db.MetodoPago, "MetodoPagoID", "Descripcion", clientes.MetodoPagoID);
            return View(clientes);
        }

        // GET: Clientes/Edit/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            ViewBag.MetodoPagoID = new SelectList(db.MetodoPago, "MetodoPagoID", "Descripcion", clientes.MetodoPagoID);
            return View(clientes);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nombre_Comercial,RNC,Cedula,Email,Telefono,Activo,MetodoPagoID")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MetodoPagoID = new SelectList(db.MetodoPago, "MetodoPagoID", "Descripcion", clientes.MetodoPagoID);
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
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
        public ActionResult exportaExcel()
        {

            string filename = "productos.csv";
            string filepath = @"c:\tmp\" + filename;
            StreamWriter sw = new StreamWriter(filepath);
            sw.WriteLine("sep=,"); //separador columnas 
            sw.WriteLine("ID, Descripcion, Estado, Label UPC, Precio, Stock, Categoria"); //Encabezado  

            foreach (var i in db.Productos.ToList())
            {

                sw.WriteLine(i.ProductoId.ToString() + "," + i.Descripcion + "," + i.Activo + "," + i.CodigoUPC + "," + i.Precio + "," + i.Stock + "," + i.Categoria.Descripcion);

            }

            sw.Close();
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition

            {

                FileName = filename,
                Inline = true,

            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType);

        }



    }
}

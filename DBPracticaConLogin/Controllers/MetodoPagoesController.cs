using DBPracticaConLogin;

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

namespace DBPracticaConLoginSearchYList.Controllers
{
    public class MetodoPagoesController : Controller
    {
        private FacturacionProdGrupooEntities db = new FacturacionProdGrupooEntities();

        // GET: MetodoPagoes
        [Authorize]
        public ActionResult Index()
        {
            return View(db.MetodoPago.ToList());
        }

        // GET: MetodoPagoes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodoPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Create
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetodoPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MetodoPagoID,Descripcion,CantidadDias,MonedaLocal,Activo")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                db.MetodoPago.Add(metodoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metodoPago);
        }

        // GET: MetodoPagoes/Edit/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodoPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // POST: MetodoPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MetodoPagoID,Descripcion,CantidadDias,MonedaLocal,Activo")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metodoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metodoPago);
        }

        // GET: MetodoPagoes/Delete/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetodoPago metodoPago = db.MetodoPago.Find(id);
            if (metodoPago == null)
            {
                return HttpNotFound();
            }
            return View(metodoPago);
        }

        // POST: MetodoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MetodoPago metodoPago = db.MetodoPago.Find(id);
            db.MetodoPago.Remove(metodoPago);
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
            sw.WriteLine("Descripcion, Es Moneda Local, Cantidad de Dias"); //Encabezado  

            foreach (var i in db.MetodoPago.ToList())
            {

                sw.WriteLine(i.Descripcion.ToString() + "," + i.MonedaLocal + "," + i.CantidadDias);

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

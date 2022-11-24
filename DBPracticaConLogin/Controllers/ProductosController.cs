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
    public class ProductosController : Controller
    {
        private FacturacionProdGrupooEntities db = new FacturacionProdGrupooEntities();

        // GET: Productos
        [Authorize]
        public ActionResult Index(string Criterio = null)
        {

            return View(db.Productos.Where(p => Criterio == null || p.Descripcion.StartsWith(Criterio) ||
            p.Precio.ToString().StartsWith(Criterio)
            || p.Categoria.Descripcion.StartsWith(Criterio)         
            || p.CodigoUPC.StartsWith(Criterio)).ToList());
        }
            //var productos = db.Productos.Include(p => p.Categoria);
            //return View(productos.ToList());
        

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Descripcion");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,CodigoUPC,Descripcion,Precio,Activo,Stock,CategoriaID")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Descripcion", productos.CategoriaID);
            return View(productos);
        }

        // GET: Productos/Edit/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Descripcion", productos.CategoriaID);
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,CodigoUPC,Descripcion,Precio,Activo,Stock,CategoriaID")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categoria, "CategoriaID", "Descripcion", productos.CategoriaID);
            return View(productos);
        }

        // GET: Productos/Delete/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productos productos = db.Productos.Find(id);
            db.Productos.Remove(productos);
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

            foreach (var i in db.Productos.ToList())            {

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

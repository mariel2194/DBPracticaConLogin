﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DBPracticaConLoginSearchYList;

namespace DBPracticaConLoginSearchYList.Controllers
{
    public class VendedoresController : Controller
    {
        private FacturacionProdGrupooEntities db = new FacturacionProdGrupooEntities();

        // GET: Vendedores
        public ActionResult Index()
        {
            return View(db.Vendedores.ToList());
        }

        // GET: Vendedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // GET: Vendedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendedorId,Nombre,Apellido,Cedula,Salario,Activo,Email,Telefono,ComisionPorVenta")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                db.Vendedores.Add(vendedores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendedores);
        }

        // GET: Vendedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VendedorId,Nombre,Apellido,Cedula,Salario,Activo,Email,Telefono,ComisionPorVenta")] Vendedores vendedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendedores);
        }

        // GET: Vendedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedores vendedores = db.Vendedores.Find(id);
            if (vendedores == null)
            {
                return HttpNotFound();
            }
            return View(vendedores);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendedores vendedores = db.Vendedores.Find(id);
            db.Vendedores.Remove(vendedores);
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
        //public ActionResult SelectComision()
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    {
        //        items.Add(new SelectListItem { Text = "Action", Value = "0" });

        //        items.Add(new SelectListItem { Text = "Drama", Value = "1" });

        //        items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

        //        items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

        //        ViewBag.Vendedores = items;

        //        return View();


        //        //new SelectListItem{ Text="Seleccione por ciento de ventas", Value = "" },
        //        //     new SelectListItem{ Text="3%", Value = "3" },
        //        //     new SelectListItem{ Text="5%", Value = "5" },
        //        //     new SelectListItem{ Text="15%", Value = "15" },
        //        //     new SelectListItem{ Text="20%", Value = "0" }




        //    }
        



        }
}

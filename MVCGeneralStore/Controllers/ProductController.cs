﻿using MVCGeneralStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCGeneralStore.Controllers
{

    public class ProductController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            List<Product> productList = _db.Products.ToList();
            List<Product> orderedList = productList.OrderBy(prod => prod.Name).ToList();
            return View(orderedList);
        }

        // Get: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product); 
        }


        // Get: Product/Delete
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Post: Delete
        // Product/Delete/{id}

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product product = _db.Products.Find(id);

            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get:Edit
        //Product/Edit{id}
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            { return HttpNotFound(); }
            return View(product);
        }
        //Post:Edit
        //Product/Edit/{id}
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //Get:Details
        //Product/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
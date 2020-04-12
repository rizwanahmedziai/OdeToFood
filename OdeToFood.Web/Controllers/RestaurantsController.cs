﻿using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData db;

        public RestaurantsController(IRestaurantData db)
        {
            this.db = db;
        }
        // GET: Restaurants
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var model = db.Get((int)id);
            if (model == null)
            {
                return View("NotFound");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            // ValidateEntry(restaurant);
            // Use Data Annotations to valid entries instead [Required]
            if (ModelState.IsValid)
            {
                db.Add(restaurant);

                //First Technique for Post-Redirect-Get pattern for form submission
                //return RedirectToAction("Index");
                //Second Technique is to redirect to details passing id object
                return RedirectToAction("Details", new { id = restaurant.Id});
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }

        private void ValidateEntry(Restaurant restaurant)
        {
            // If there aren't any errors
            // A better way is to use data annotations 
     
            if (String.IsNullOrEmpty(restaurant.Name))
            {
                ModelState.AddModelError("Name",
                    "The Name field value cannot be empty!");
            }
        }

    }
}
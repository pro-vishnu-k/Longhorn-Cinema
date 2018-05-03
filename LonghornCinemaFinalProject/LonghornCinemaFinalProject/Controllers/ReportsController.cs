﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LonghornCinemaFinalProject.DAL;
using LonghornCinemaFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace LonghornCinemaFinalProject.Controllers
{
    public class ReportsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reports
        //public ActionResult Index()
        //{
        //    ViewBag.AllMovies = GetAllMovies();
        //    return View(db.Reports.ToList());
        //}

        // GET: Query page
        public ActionResult Query() // represents a view, not an actual query action
        {
            ViewBag.AllMovies = GetAllMovies();
            ViewBag.AllCustomers = GetAllCustomers();
            return View();
        }

        // POST: Query
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Query([Bind(Include = "ReportID,StartDate,EndDate,DisplaySeats,DisplayRevenue,MovieFilter,RatingFilter,CustomerFilter")] Report report, Int32 MovieID, Int32? CustomerFilterID)
        {
            AppUser FilterUser = db.Users.Find(CustomerFilterID);
            Movie movie = db.Movies.Find(MovieID);
            report.CustomerFilter = FilterUser;

            var query = from t in db.Tickets select t;
            if (report.StartDate != null)
                query = query.Where(t => t.Order.OrderDate > report.StartDate);
            if (report.EndDate != null)
                query = query.Where(t => t.Order.OrderDate < report.EndDate);
            if (MovieID != 0)
                query = query.Where(t => t.Showing.Movie.MovieID == MovieID);
            //if (report.MovieFilter != null)
            //    query = query.Where(t => t.Showing.Movie.MovieID == report.MovieFilter.MovieID);
            if (report.RatingFilter != MPAARating.None)
                query = query.Where(t => t.Showing.Movie.MPAARating == report.RatingFilter);
            if (CustomerFilterID != null)
                query = query.Where(t => t.Order.AppUser.Id == CustomerFilterID.ToString());

            ViewBag.SeatsSold = query.Count();
            if (query.Count() > 0)
                ViewBag.Revenue = query.Sum(t => t.TicketPrice);
            else
                ViewBag.Revenue = 0;

            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Results", new { ReportID = report.ReportID });
            }
            else throw new HttpException(400, "Bad Request");

            ViewBag.AllMovies = GetAllMovies();
            ViewBag.AllCustomers = GetAllCustomers();

            return View(report);
        }

        public ActionResult Results(int ReportID)
        {
            Report report = db.Reports.Find(ReportID);
            if (report == null)
            {
                return HttpNotFound();
            }

            return View("~/Views/Reports/Results.cshtml", report);

        }

        public SelectList GetAllMovies()
        {
            Movie custom = new Movie("All Movies");
            List<Movie> Movies = new List<Movie> { custom };
            SelectList AllMovies = new SelectList(Movies.OrderBy(m => m.MovieID), "MovieID", "Title");
            Movies.AddRange(db.Movies);

            return AllMovies;
        }

        public SelectList GetAllCustomers()
        {
            //AppUser custom = new AppUser("All Customers");
            List<AppUser> Customers = db.Users.ToList();

            //SelectListItem s = new SelectListItem() { Text = "All Customers", Value = "0" };
            
            //SelectList AllCustomers = new SelectList( IEnumerable(s) ) ;

            SelectList AllCustomers = new SelectList(Customers.OrderBy(m => m.LastName), "Id", "Email");
            return AllCustomers;
        }

        //// GET: Reports/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Report report = db.Reports.Find(id);
        //    if (report == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(report);
        //}

        // GET: Reports/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ReportID,DisplaySeats,DisplayRevenue,StartDate,EndDate,RatingFilter")] Report report)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Reports.Add(report);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(report);
        //}

        // GET: Reports/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Report report = db.Reports.Find(id);
        //    if (report == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(report);
        //}

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ReportID,DisplaySeats,DisplayRevenue,StartDate,EndDate,RatingFilter")] Report report)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(report).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(report);
        //}

        // GET: Reports/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Report report = db.Reports.Find(id);
        //    if (report == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(report);
        //}

        // POST: Reports/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Report report = db.Reports.Find(id);
        //    db.Reports.Remove(report);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

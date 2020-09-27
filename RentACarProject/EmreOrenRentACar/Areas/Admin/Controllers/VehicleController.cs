using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmreOrenRentACar.Areas.Admin.Data;
using EmreOrenRentACar.Models;

namespace EmreOrenRentACar.Areas.Admin.Controllers
{
    public class VehicleController : Controller
    {
        private RentalContext db = new RentalContext();

        // GET: Admin/Vehicle
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.VehicleType);
            return View(vehicles.ToList());
        }

        // GET: Admin/Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Admin/Vehicle/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Admin/Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,Year,Image,DailyPrice,VehicleTypeId")] VehicleDto input)
        {
            if (ModelState.IsValid)
            {
                string newfoto = null;
                if (input.Image != null && input.Image.ContentLength > 0)
                {
                    FileInfo fotoinfo = new FileInfo(input.Image.FileName);
                    newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;

                    string path = Path.Combine(Server.MapPath("~/Upload"),
                                               Path.GetFileName(newfoto));
                    input.Image.SaveAs(path);
                }
                var vehicle = new Vehicle()
                {
                    Brand = input.Brand,
                    DailyPrice = input.DailyPrice,
                    Image = newfoto != null ? ("/Upload/" + newfoto) : null,
                    Model = input.Model,
                    VehicleTypeId = input.VehicleTypeId,
                    Year = input.Year
                };

                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", input.VehicleTypeId);
            return View(input);
        }

        // GET: Admin/Vehicle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicle = db.Vehicles.Find(id);
            var result = new VehicleDto()
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                DailyPrice = vehicle.DailyPrice,
                OldImage = vehicle.Image,
                Image = null,
                Model = vehicle.Model,
                VehicleTypeId = vehicle.VehicleTypeId,
                Year = vehicle.Year
            };
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", result.VehicleTypeId);
            return View(result);
        }

        // POST: Admin/Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,Year,Image,OldImage,DailyPrice,VehicleTypeId")] VehicleDto input)
        {
            if (ModelState.IsValid)
            {
                string newfoto = null;
                if (input.Image != null && input.Image.ContentLength > 0)
                {
                    FileInfo fotoinfo = new FileInfo(input.Image.FileName);
                    newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;

                    string path = Path.Combine(Server.MapPath("~/Upload"),
                                               Path.GetFileName(newfoto));
                    input.Image.SaveAs(path);
                }
                var vehicle = db.Vehicles.Include(y => y.VehicleType).FirstOrDefault(x => x.Id == input.Id);
                if (vehicle == null)
                {
                    Create(input);
                }
                else
                {
                    vehicle.Brand = input.Brand;
                    vehicle.DailyPrice = input.DailyPrice;
                    vehicle.Image = newfoto != null ? ("/Upload/" + newfoto) : input.OldImage;
                    vehicle.Model = input.Model;
                    vehicle.VehicleTypeId = input.VehicleTypeId;
                    vehicle.Year = input.Year;
                }

                db.Vehicles.Add(vehicle);
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", input.VehicleTypeId);
            return View(input);
        }

        // GET: Admin/Vehicle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Admin/Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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

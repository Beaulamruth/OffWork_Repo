using OffWork.DataAccess;
using OffWork.Models;
using OffWork.Services;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OffWork.Controllers
{
    public class AbsenceDetailsController : Controller
    {
        private OffWorkContext db = new OffWorkContext();
        private AbsenceDetailService service = new AbsenceDetailService();

        // GET: AbsenceDetails
        public ActionResult Index(int? employeeId, DateTime? startDate, DateTime? endDate)
        {
            var absenceDetail = db.AbsenceDetail.Include(a => a.AbsenceType).Include(a => a.Employee);

            var allEmployeeList = new SelectList(db.Employee, "EmployeeId", "FullName");

            ViewBag.FilteredEmployeeList = allEmployeeList;
            if (employeeId.HasValue && employeeId.ToString() != "0" && employeeId != 0)
            {
                absenceDetail = absenceDetail.Where(s => s.EmployeeId == employeeId);
                ViewBag.FilteredEmployeeList = allEmployeeList.Where(e => Convert.ToInt32(e.Value) == employeeId);
            }
            if (startDate.HasValue)
            {
                absenceDetail = absenceDetail.Where(s => s.StartDateOfAbsence >= startDate);
            }
            if (endDate.HasValue)
            {
                absenceDetail = absenceDetail.Where(s => s.EndDateOfAbsence <= endDate);
            }
            var allEmployeesSorted = allEmployeeList.OrderBy(x => Convert.ToInt32(x.Value));
            ViewBag.EmployeeList = allEmployeesSorted;

            return View(absenceDetail.ToList());
        }

        // GET: AbsenceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbsenceDetail absenceDetail = db.AbsenceDetail.Find(id);
            if (absenceDetail == null)
            {
                return HttpNotFound();
            }
            return View(absenceDetail);
        }

        // GET: AbsenceDetails/Create
        public ActionResult Create()
        {
            ViewBag.AbsenceTypeList = new SelectList(db.AbsenceType, "AbsenceTypeId", "AbsenceTypeValue");
            ViewBag.EmployeeList = new SelectList(db.Employee, "EmployeeId", "FullName");
            AbsenceDetail absenceDetail = new AbsenceDetail();
            return View(absenceDetail);
        }

        // POST: AbsenceDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AbsenceDetailId,EmployeeId,AbsenceTypeId,StartDateOfAbsence,EndDateOfAbsence,StartDateFullDay,EndDateFullDay,IsApproved")] AbsenceDetail absenceDetail)
        {
            Employee Employee = db.Employee.Find(absenceDetail.EmployeeId);
            if(Employee.HireDate>absenceDetail.StartDateOfAbsence)
            {
                ModelState.AddModelError("", "Absent From  must be greater than Hired date.");
            }
            if (absenceDetail.EndDateOfAbsence < absenceDetail.StartDateOfAbsence)
            {
                ModelState.AddModelError("", "Absent Till must be greater than Absent From.");
            }
                if (ModelState.IsValid)
            {
                db.AbsenceDetail.Add(absenceDetail);
                db.SaveChanges();
                service.NotifyUpdates(absenceDetail);
                return RedirectToAction("Index");
            }

            ViewBag.AbsenceTypeId = new SelectList(db.AbsenceType, "AbsenceTypeId", "AbsenceTypeValue", absenceDetail.AbsenceTypeId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "FirstName", absenceDetail.EmployeeId);
            return View(absenceDetail);
        }

        // GET: AbsenceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbsenceDetail absenceDetail = db.AbsenceDetail.Find(id);
            if (absenceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.AbsenceTypeId = new SelectList(db.AbsenceType, "AbsenceTypeId", "AbsenceTypeValue", absenceDetail.AbsenceTypeId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "FirstName", absenceDetail.EmployeeId);
            
            return View(absenceDetail);
        }

        // POST: AbsenceDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbsenceDetailId,EmployeeId,AbsenceTypeId,StartDateOfAbsence,EndDateOfAbsence,StartDateFullDay,EndDateFullDay,IsApproved")] AbsenceDetail absenceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(absenceDetail).State = EntityState.Modified;
                db.SaveChanges();
                service.NotifyUpdates(absenceDetail);
                return RedirectToAction("Index");
            }
            ViewBag.AbsenceTypeId = new SelectList(db.AbsenceType, "AbsenceTypeId", "AbsenceTypeValue", absenceDetail.AbsenceTypeId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "FirstName", absenceDetail.EmployeeId);
            return View(absenceDetail);
        }

        // GET: AbsenceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbsenceDetail absenceDetail = db.AbsenceDetail.Find(id);
            if (absenceDetail == null)
            {
                return HttpNotFound();
            }
            return View(absenceDetail);
        }

        // POST: AbsenceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AbsenceDetail absenceDetail = db.AbsenceDetail.Find(id);
            db.AbsenceDetail.Remove(absenceDetail);
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

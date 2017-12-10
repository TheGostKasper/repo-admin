using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AMS.Context;
using AMS.Models;

namespace AMS.Controllers
{
    [Authorize]
    public class EmployeeRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeRole
        public async Task<ActionResult> Index()
        {
            return View(await db.EmployeeRoles.ToListAsync());
        }

        // GET: EmployeeRole/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = await db.EmployeeRoles.FindAsync(id);
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            return View(employeeRole);
        }

        // GET: EmployeeRole/Create
        public async Task<ActionResult> Create()
        {
            ViewData["EmployeesLookup"] = await db.Employees.ToListAsync();
            ViewData["RolesLookup"] = await db.Roles.ToListAsync();
            return View();
        }

        // POST: EmployeeRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmpId,RoleId")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeRoles.Add(employeeRole);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employeeRole);
        }

        // GET: EmployeeRole/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = await db.EmployeeRoles.FindAsync(id);
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            ViewData["EmployeesLookup"] = await db.Employees.ToListAsync();
            ViewData["RolesLookup"] = await db.Roles.ToListAsync();
            return View(employeeRole);
        }

        // POST: EmployeeRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmpId,RoleId")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeRole).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employeeRole);
        }

        // GET: EmployeeRole/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = await db.EmployeeRoles.FindAsync(id);
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            return View(employeeRole);
        }

        // POST: EmployeeRole/Delete/5
        [Authorize(Roles ="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EmployeeRole employeeRole = await db.EmployeeRoles.FindAsync(id);
            db.EmployeeRoles.Remove(employeeRole);
            await db.SaveChangesAsync();
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

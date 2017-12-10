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
using AMS.Helper;
using log4net;

namespace AMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = await db.Employees.FindAsync(id);
            employee.Password = VXSecurity.Decrypt(employee.Password);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public async Task<ActionResult> Create()
        {
            ViewData["Roles"] = await db.Roles.ToListAsync();

            ViewData["AccountStateLookup"] = await db.AccountStates.ToListAsync();
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Mobile,Email,Password,Address,Status,CreationDate,RolesIds")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    Password = VXSecurity.Encrypt(employee.Password),
                    CreationDate = DateTime.UtcNow,
                    Address = employee.Address,
                    Mobile = employee.Mobile,
                    Status = employee.Status
                });
                await db.SaveChangesAsync();
                AddRoles(employee.Email, employee.Mobile, employee.RolesIds);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        public void AddRoles(string email, string mobile, string rolesIds)
        {
            try
            {
                var empId = db.Employees.FirstOrDefault(e => e.Email == email && e.Mobile == mobile);
               
                
                    if (empId != null && rolesIds != null)
                    {
                        var roleArr = rolesIds.Split(',');
                        if (roleArr.Length > 0)
                        {
                            for (int i = 0; i < roleArr.Length - 1; i++)
                            {
                                db.EmployeeRoles.Add(new EmployeeRole
                                {
                                    EmpId = empId.Id,
                                    RoleId = int.Parse(roleArr[i])
                                });
                                db.SaveChanges();
                            }
                        }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = await db.Employees.FindAsync(id);
            employee.Password = VXSecurity.Decrypt(employee.Password);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewData["Roles"] = await db.Roles.ToListAsync();
            ViewData["EmpRoles"] = await db.EmployeeRoles.Where(e => e.EmpId == id.Value).ToListAsync();
            ViewData["AccountStateLookup"] = await db.AccountStates.ToListAsync();

            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Mobile,Email,Password,Address,Status,CreationDate")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employee.Password = VXSecurity.Encrypt(employee.Password);
                    db.Entry(employee).State = EntityState.Modified;
                    db.Entry(employee).Property(p => p.CreationDate).IsModified = false;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(employee);
                Log.Error(ex.Message, ex);
                return View(employee);
            }

        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await db.Employees.FindAsync(id);
                db.Employees.Remove(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Message = "Cannot Delete Employee", id = id });
                Log.Error(ex.Message, ex);
                throw;
            }

        }


        [HttpPost]
        public void UpdateEmployeeRole(int empId, int roleId)
        {
            try
            {
                using (var _db = new ApplicationDbContext())
                {
                    var empRole = _db.EmployeeRoles.FirstOrDefault(e => e.EmpId == empId && e.RoleId == roleId);
                    if (empRole == null)
                    {
                        _db.EmployeeRoles.Add(new EmployeeRole
                        {
                            EmpId = empId,
                            RoleId = roleId
                        });
                    }
                    else
                    {
                        _db.EmployeeRoles.Remove(empRole);
                    }
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ThreadContext.Properties["data"] = VXConverter.ObjectToJson(new { Message = "Cannot update Employee Role", empId = empId, roleId = roleId });
                Log.Error(ex.Message, ex);
            }

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

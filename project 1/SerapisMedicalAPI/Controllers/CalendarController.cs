using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SerapisMedicalAPI.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return null;
        }

        // GET: Calendar/Details/5
        public ActionResult Details(int id)
        {
            return null;
        }

        // GET: Calendar/Create
        public ActionResult Create()
        {
            return null;
        }

        // POST: Calendar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
            }
        }

        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            return null;
        }

        // POST: Calendar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
            }
        }

        // GET: Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return null;
        }

        // POST: Calendar/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
            }
        }
    }
}
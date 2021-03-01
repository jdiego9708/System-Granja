using SISGranja.API.Repository;
using SISGranja.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SISGranja.API.Controllers
{
    public class CorralesController : Controller
    {
        CorralRepository repository = new CorralRepository();

        // GET: Corrales
        public async Task<ActionResult> ObservarCorrales()
        {
            var json = await repository.GetCorrales("COMPLETO", "");

            return Content(json, "application/json", System.Text.Encoding.UTF8);
        }

        // GET: Corrales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Corrales/Create
        public ActionResult CrearCorral()
        {
            return View();
        }

        // POST: Corrales/Create
        [HttpPost]
        public async Task<ActionResult> CrearCorral(Corrales corral)
        {
            try
            {
                var (rpta, jsonModel) = await repository.InsertCorral(corral);

                if (rpta.Equals("OK"))
                    return Content(jsonModel, "application/json", System.Text.Encoding.UTF8);
                else
                    return Content("{}", "application/json", System.Text.Encoding.UTF8);
            }
            catch
            {
                return Content("{}", "application/json", System.Text.Encoding.UTF8);
            }
        }

        // GET: Corrales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Corrales/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Corrales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Corrales/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

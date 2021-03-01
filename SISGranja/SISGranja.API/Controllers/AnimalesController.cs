using Newtonsoft.Json;
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
    public class AnimalesController : Controller
    {
        AnimalRepository repository = new AnimalRepository();

        // GET: Animales
        public async Task<ActionResult> ObservarAnimales()
        {
            var json = await repository.GetAnimals("COMPLETO", "");

            return Content(json, "application/json", System.Text.Encoding.UTF8);
        }

        // GET: Animales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Animales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Animales/Create
        [HttpPost]
        public async Task<ActionResult> Create(Animales animal)
        {
            try
            {
                var (rpta, jsonModel) = await repository.InsertAnimal(animal);

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

        // GET: Animales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Animales/Edit/5
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

        // GET: Animales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Animales/Delete/5
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

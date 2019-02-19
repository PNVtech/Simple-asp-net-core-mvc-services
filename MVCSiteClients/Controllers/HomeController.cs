using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCSiteClients.Models;
using MVCSiteClients.ModuleCode;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCSiteClients.Controllers
{
    public class HomeController : Controller
    {
        // Получить клиентов (всех или с фильтром по городу)
        public IActionResult Index(int? CityID)
        {
            IEnumerable<City> CitiesList = WebActions.GetCities().Result;
            ViewData["Cities"] = new SelectList(CitiesList, "Id", "Name");

            if (!CityID.HasValue)
            {
                var Model = WebActions.GetClients().Result;
                ViewData["HeaderPage"] = "Клиенты";
                
                return View(Model);
            }
            else
            {
                var Model = WebActions.GetClientsFromCity(CityID).Result;

                if (Model.Count() != 0)
                {
                    ViewData["HeaderPage"] = $"Клиенты из города {Model.ElementAtOrDefault(0).City}";
                    return View(Model);
                }
                else
                {
                    return NotFound($"Клиенты из города {CitiesList.FirstOrDefault(c => c.Id == CityID).Name} не найдены.");
                }
            }
        }

        // Получить определённого клиента
        public IActionResult EditClient(int? ClientID)
        {
            if (ClientID.HasValue)
            {
                var Model = WebActions.GetClient(ClientID);
                IEnumerable<City> CitiesList = WebActions.GetCities().Result;
                ViewData["Cities"] = new SelectList(CitiesList, "Id", "Name");
                return View("ClientView", Model.Result);
            }
            else
            {
                IEnumerable<City> CitiesList = WebActions.GetCities().Result;
                ViewData["Cities"] = new SelectList(CitiesList, "Id", "Name");
                ClientView NewClient = new ClientView();
                return View("ClientView", NewClient);
            }
        }

        // Редактировать клиента
        [HttpPost]
        public IActionResult SaveClient(ClientView CurrenClient)
        {
            if (ModelState.IsValid)
            {
                if (CurrenClient.Id == 0)
                {
                    //post client
                    WebActions.AddClient(CurrenClient);
                    return RedirectToAction("Index");
                }
                else
                {
                    //put client
                    WebActions.EditClient(CurrenClient);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // Удалить клиента
        [HttpGet]
        public IActionResult DeleteClient(int? ClientID)
        {
            if (ClientID.HasValue)
            {
                WebActions.DeleteClient(ClientID);
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
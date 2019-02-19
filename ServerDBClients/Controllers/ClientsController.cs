using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerDBClients.Models;
using ServerDBClients.ModuleCode;

namespace ServerDBClients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private ClientsDBContext DB;
        private DataActions DA;

        public ClientsController(ClientsDBContext db)
        {
            this.DB = db;
            DA = new DataActions(this.DB);
        }

        // Получить всех клиентов
        // GET api/Clients
        [HttpGet]
        public ActionResult<IEnumerable<ClientView>> GetAllClients()
        {
            return DA.GetClients().ToArray();
        }

        // Получить клиентов из города по ID
        // GET /api/ClientsFromCity/5
        [Route("/api/ClientsFromCity/{CityID}")]
        [HttpGet]
        public ActionResult<IEnumerable<ClientView>> GetClientsFromCity([FromRoute] int? CityID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (CityID.HasValue)
                {
                    return DA.GetClientsFromCity(CityID).ToArray();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // Получить определённого клиента
        // GET api/Clients/5
        [HttpGet("{id}")]
        public ActionResult<ClientView> GetClient([FromRoute] int? id)
        {
            if (id.HasValue)
            {
                ClientView SelectedClient = DA.GetClientViewById(id);
                return SelectedClient;
            }
            else
            {
                return BadRequest();
            }
        }

        // Добавить клиента
        // POST api/Clients
        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] Client NewClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await DA.SaveClient(NewClient);
                return Ok();
            }
        }

        // Редактировать клиента
        // PUT api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] Client SelectedClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (id != SelectedClient.Id)
                {
                    return BadRequest();
                }
                else
                {
                    await DA.SaveClient(SelectedClient);
                    return Ok();
                }
            }
        }

        // Удалить клиента
        // DELETE api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (id.HasValue)
                {
                    await DA.DeleteClient(id);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // Получить все города
        // /api/Cities
        [Route("/api/Cities")]
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            return DA.GetCities().ToArray();
        }
    }
}

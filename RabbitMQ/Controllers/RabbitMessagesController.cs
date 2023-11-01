using Microsoft.AspNetCore.Mvc;
using Rabbit.Models.Entities;
using Rabbit.Services.Interfaces;

namespace RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMessagesController : Controller
    {
        public readonly IRabbitMessageService _service;

        public RabbitMessagesController(IRabbitMessageService service) 
        {
            _service = service;
        }

        [HttpPost]
        public void AddMessage(RabbitMessage message) 
        { 
            _service.SendMessage(message);
        }
    }
}

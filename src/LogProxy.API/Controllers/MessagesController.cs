using LogProxy.Core.DTO;
using LogProxy.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxy.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<MessageDTO> Add(NewMessageDTO message)
        {
            return await messageService.Add(message);
        }

        [HttpGet]
        public async Task<List<MessageDTO>> GetAll()
        {
            return await messageService.GetAll();
        }
    }
}

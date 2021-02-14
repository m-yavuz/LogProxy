using LogProxy.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.Core.Interfaces
{
    public interface IMessageService
    {
        Task Add(NewMessageDTO message);

        Task<List<MessageDTO>> GetAll();
    }
}

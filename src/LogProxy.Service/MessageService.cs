using LogProxy.Core.AppSettings;
using LogProxy.Core.DTO;
using LogProxy.Core.Interfaces;
using LogProxy.Service.Utilities;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogProxy.Service
{
    public class MessageService : IMessageService
    {
        private readonly IAirTableClient airTableClient;

        public MessageService(IAirTableClient airTableClient)
        {
            this.airTableClient = airTableClient;
        }

        public async Task<MessageDTO> AddAsync(NewMessageDTO message)
        {
            var model = await airTableClient.AddMessages(new NewDataSet(message));
            return model.Records.Select(c => c.GetMessageDTO()).FirstOrDefault();
        }

        public async Task<List<MessageDTO>> GetAllAsync()
        {
            var models = await airTableClient.GetMessages();
            return models.Records.Select(c => c.GetMessageDTO()).ToList();
        }
    }
}

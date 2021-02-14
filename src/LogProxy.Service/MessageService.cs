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
        private readonly AirTableSettings airTableSettings;
        private readonly IAirTableService airTableService;
        public MessageService(IOptions<AppSettings> options)
        {
            airTableSettings = options.Value.AirTableSettings;

            var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(options.Value.AirTableSettings.Key)
            };

            airTableService = RestService.For<IAirTableService>(airTableSettings.URL, refitSettings);
        }

        public async Task Add(NewMessageDTO message)
        {
            try
            {
                await airTableService.AddMessages(new DataSet(message));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<MessageDTO>> GetAll()
        {
            var models = await airTableService.GetMessages();
            return models.Records.Select(c => c.Fields.GetMessageDTO()).ToList();
        }
    }
}

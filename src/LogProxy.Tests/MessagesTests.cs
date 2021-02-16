using LogProxy.API.Controllers;
using LogProxy.Core.DTO;
using LogProxy.Core.Interfaces;
using LogProxy.Service;
using LogProxy.Service.Utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogProxy.Tests
{
    public class MessagesTests
    {
        private MessagesController controller;
        private IMessageService messageService;
        private Mock<IAirTableClient> airTableClienteMock;

        private FetchDataSet fetchDataSet = new FetchDataSet();


        [SetUp]
        public void Setup()
        {
            airTableClienteMock = new Mock<IAirTableClient>();
            airTableClienteMock.Setup(c => c.AddMessages(It.IsAny<NewDataSet>()))
                               .ReturnsAsync((NewDataSet d) => new FetchDataSet()
                               {
                                   Records = new List<FetchRecord>() { new FetchRecord() { Fields = new FetchRow() {
                                   Message = d.Records[0].Fields.Message, Summary = d.Records[0].Fields.Summary, } } }
                               });

            airTableClienteMock.Setup(c => c.GetMessages(3, "Grid view"))
                               .ReturnsAsync(fetchDataSet);

            setupDataResult();

            messageService = new MessageService(airTableClienteMock.Object);
            controller = new MessagesController(messageService);
        }

        private void setupDataResult()
        {
            fetchDataSet.Records = new List<FetchRecord>();
            fetchDataSet.Records.Add(new FetchRecord()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.UtcNow,
                Fields = new FetchRow()
                {
                    Message = "Message1",
                    Summary = "Summary1"
                }
            });

            fetchDataSet.Records.Add(new FetchRecord()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedTime = DateTime.UtcNow,
                Fields = new FetchRow()
                {
                    Message = "Message2",
                    Summary = "Summary2"
                }
            });
        }

        [Test]
        public async Task After_adding_new_message_to_controller_Should_back_AS_returen()
        {
            var newMessage = new NewMessageDTO()
            {
                Title = "Title",
                Text = "New Text "
            };

            var result = await controller.Add(newMessage);

            Assert.AreEqual(newMessage.Text, result.Text);
            Assert.AreEqual(newMessage.Title, result.Title);
        }

        [Test]
        public async Task Get_all_messages_in_client_test_mapping()
        {
            var result = await controller.GetAll();

            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result[0].Text, fetchDataSet.Records[0].Fields.Message);
            Assert.AreEqual(result[0].Title, fetchDataSet.Records[0].Fields.Summary);

        }
    }
}
using LogProxy.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LogProxy.Service.Utilities
{
    /// <summary>
    /// Represents AirTable Models
    /// </summary>
    public class FetchRow
    {
        public FetchRow() { }

        public string Id { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }

        public MessageDTO GetMessageDTO()
        {
            return new MessageDTO()
            {
                Id = Id,
                Title = Summary,
                Text = Message,
                ReceivedAt = ReceivedAt,
            };
        }
    }

    public class FetchRecord
    {
        public string Id { get; set; }
        public FetchRow Fields { get; set; } = new FetchRow();
        public DateTime CreatedTime { get; set; }
    }

    public class FetchDataSet
    {
        public FetchDataSet()
        {

        }

        public List<FetchRecord> Records { get; set; } = new List<FetchRecord>();
    }



    public class NewRow
    {
        public NewRow() { }

        public NewRow(NewMessageDTO message)
        {
            Summary = message.Title;
            Message = message.Text;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("Summary")]
        public string Summary { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

    }

    public class NewRecord
    {
        public NewRecord()
        {

        }

        public NewRecord(NewMessageDTO message)
        {
            Fields = new NewRow(message);
        }

        public NewRow Fields { get; set; } = new NewRow();
    }

    public class NewDataSet
    {
        public NewDataSet() { }

        public NewDataSet(NewMessageDTO message)
        {
            Records = new List<NewRecord>();
            Records.Add(new NewRecord(message));
        }

        public List<NewRecord> Records { get; set; } = new List<NewRecord>();
    }
}

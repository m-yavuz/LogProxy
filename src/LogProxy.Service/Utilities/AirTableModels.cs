using LogProxy.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Service.Utilities
{
    /// <summary>
    /// Represents AirTable Models
    /// </summary>
    public class Row
    {
        public Row() { }

        public Row(NewMessageDTO message)
        {
            Summary = message.Title;
            Message = message.Text;
        }

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

    public class Record
    {
        public Record()
        {

        }

        public Record(NewMessageDTO message)
        {
            Fields = new Row(message);
        }

        public string Id { get; set; }
        public Row Fields { get; set; } = new Row();
        public DateTime CreatedTime { get; set; }
    }

    public class DataSet
    {
        public DataSet()
        {
            
        }

        public DataSet(NewMessageDTO message)
        {
            Records = new List<Record>();
            Records.Add(new Record(message));
        }

        public List<Record> Records { get; set; } = new List<Record>();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Core.DTO
{
    public class MessageDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}

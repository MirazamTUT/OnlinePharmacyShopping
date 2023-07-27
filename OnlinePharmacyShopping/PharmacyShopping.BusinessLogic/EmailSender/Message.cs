﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.EmailSender
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, string name)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(name, x)));
            Subject = subject;
            Content = content;
            Name = name;
        }
    }
}

﻿using EmailSender.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Interfaces
{
    public interface IEmailSenderService
    {
        Task<(bool success, string errorMsg)> SendEmailAsync(MailboxAddress sender, MailboxAddress[] recepients, string subject, string body, SmtpConfig config = null, bool isHtml = true);
        Task<(bool success, string errorMsg)> SendEmailAsync(string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null, bool isHtml = true);
        Task<(bool success, string errorMsg)> SendEmailAsync(string senderName, string senderEmail, string recepientName, string recepientEmail, string subject, string body, SmtpConfig config = null, bool isHtml = true);
    }
}

﻿namespace UniTutor.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string subject, string body);
    }
}

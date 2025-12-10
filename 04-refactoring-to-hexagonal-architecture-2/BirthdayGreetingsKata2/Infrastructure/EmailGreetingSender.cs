using System.Collections.Generic;
using System.Net.Mail;
using BirthdayGreetingsKata2.Core;

namespace BirthdayGreetingsKata2.Application;

public class EmailGreetingSender : GreetingSender
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _sender;

    public EmailGreetingSender(string smtpHost, int smtpPort, string sender)
    {
        _smtpHost = smtpHost;
        _smtpPort = smtpPort;
        _sender = sender;
    }

    public void Send(List<GreetingMessage> messages)
    {
        foreach (var message in messages)
        {
            SendMessage(message);
        }
    }

    private void SendMessage(GreetingMessage message)
    {
        var smtpClient = CreateMailSession();
        var msg = ConstructMessage(message);
        SendMessage(msg, smtpClient);
    }

    private MailMessage ConstructMessage(GreetingMessage message)
    {
        var msg = new MailMessage
        {
            From = new MailAddress(_sender),
            Subject = message.Subject(),
            Body = message.Text()
        };
        msg.To.Add(message.To());
        return msg;
    }

    private SmtpClient CreateMailSession()
    {
        var smtpClient = new SmtpClient(_smtpHost)
        {
            Port = _smtpPort
        };
        return smtpClient;
    }

    protected virtual void SendMessage(MailMessage msg, SmtpClient smtpClient)
    {
        smtpClient.Send(msg);
    }
}
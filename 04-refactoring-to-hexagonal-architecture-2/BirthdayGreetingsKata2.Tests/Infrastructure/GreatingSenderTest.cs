using System.Collections.Generic;
using System.Net.Mail;
using BirthdayGreetingsKata2.Application;
using BirthdayGreetingsKata2.Core;
using BirthdayGreetingsKata2.Infrastructure.Repositories;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Infrastructure;

public class EmailGreatingSenderTest
{
    private const int SmtpPort = 25;
    private const string SmtpHost = "localhost";
    private const string From = "sender@here.com";
    private List<MailMessage> _messagesSent;
    private BirthdayService _service;
    private EmailGreetingSender _adapter;
    private const string EmployeesFilePath = "Application/employee_data.txt";

    private class EmailGreetingSenderForTesting : EmailGreetingSender
    {
        private readonly List<MailMessage> _messagesSent;

        public EmailGreetingSenderForTesting(List<MailMessage> messagesSent, int smtpPort, string smtpHost, string sender) : base(smtpHost, smtpPort, sender)
        {
            _messagesSent = messagesSent;
        }

        protected override void SendMessage(MailMessage msg, SmtpClient smtpClient)
        {
            _messagesSent.Add(msg);
        }
    }
    
    [SetUp]
    public void SetUp()
    {
        _messagesSent = new List<MailMessage>();
        var emailGreetingSender = new EmailGreetingSenderForTesting(_messagesSent, SmtpPort, SmtpHost, From);
        _service = new BirthdayService(new FileEmployeesRepository(EmployeesFilePath), emailGreetingSender);
        _adapter = emailGreetingSender; 
    }

    [Test]
    public void Will_Send_Email_One_Message()
    {
        IEnumerable<Employee> employees = new List<Employee>()
        {
            new Employee("John", "bar", OurDate("1990/01/31"), "john.doe@foobar.com")
        };
        
        var messages = GreetingMessage.GenerateForSome(employees);
        
        _adapter.Send(messages);

        Assert.That(_messagesSent, Has.Exactly(1).Items);
        var message = _messagesSent[0];
        Assert.That(message.Body, Is.EqualTo("Happy Birthday, dear John!"));
        Assert.That(message.Subject, Is.EqualTo("Happy Birthday!"));
        Assert.That(message.To, Has.Exactly(1).Items);
        Assert.That(message.To[0].Address, Is.EqualTo("john.doe@foobar.com"));
    }
    
    [Test]
    public void Will_Send_Email_Many_Message()
    {
        IEnumerable<Employee> employees = new List<Employee>()
        {
            new Employee("John", "bar", OurDate("1990/01/31"), "john.doe@foobar.com"),
            new Employee("John2", "bar2", OurDate("1992/01/31"), "john.doe2@foobar.com")
        };
        
        var messages = GreetingMessage.GenerateForSome(employees);
        
        _adapter.Send(messages);

        Assert.That(_messagesSent, Has.Exactly(2).Items);
    }
    
    [Test]
    public void Will_Send_Email_Many_Different_Messages()
    {
        IEnumerable<Employee> employees = new List<Employee>()
        {
            new Employee("John", "bar", OurDate("1990/01/31"), "john.doe@foobar.com"),
            new Employee("John2", "bar2", OurDate("1992/01/31"), "john.doe2@foobar.com")
        };
        
        var messages = GreetingMessage.GenerateForSome(employees);
        
        _adapter.Send(messages);

        Assert.That(_messagesSent, Has.Exactly(2).Items);
        var message = _messagesSent[0];
        var message2 = _messagesSent[1];
        
        Assert.That(message2.Body, Is.Not.EqualTo(message.Body));
    }

    [Test]
    public void Will_Not_Send_Emails_When_No_Messages()
    {
        IEnumerable<Employee> employees = new List<Employee>();
        
        var messages = GreetingMessage.GenerateForSome(employees);
        
        _adapter.Send(messages);

        Assert.That(_messagesSent, Is.Empty);
    }
}
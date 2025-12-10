using System.Collections.Generic;
using BirthdayGreetingsKata2.Application;
using BirthdayGreetingsKata2.Core;
using BirthdayGreetingsKata2.Infrastructure.Repositories;
using NSubstitute;
using NUnit.Framework;
using static BirthdayGreetingsKata2.Tests.helpers.OurDateFactory;

namespace BirthdayGreetingsKata2.Tests.Application;

public class AcceptanceUnitTest
{
    private BirthdayService _service;
    private GreetingSender _emailGreetingSender;
    private const string EmployeesFilePath = "Application/employee_data.txt";

    [SetUp]
    public void SetUp()
    {
        _emailGreetingSender = Substitute.For<GreetingSender>();
        _service = new BirthdayService(new FileEmployeesRepository(EmployeesFilePath), _emailGreetingSender);
    }

    [Test]
    public void Base_Scenario()
    {
        var today = OurDate("2008/10/08");

        _service.SendGreetings(today);
        
        _emailGreetingSender.Received().Send(Arg.Is<List<GreetingMessage>>(messages =>  messages.Count == 1));
    }

    // [Test]
    // public void Will_Not_Send_Emails_When_Nobodies_Birthday()
    // {
    //     var today = OurDate("2008/01/01");
    //
    //     _service.SendGreetings(today);
    //
    //     Assert.That(_messagesSent, Is.Empty);
    // }
}
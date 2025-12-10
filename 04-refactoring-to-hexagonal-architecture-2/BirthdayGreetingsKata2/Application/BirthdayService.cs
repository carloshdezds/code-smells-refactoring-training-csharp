using System.Collections.Generic;
using BirthdayGreetingsKata2.Core;

namespace BirthdayGreetingsKata2.Application;

public class BirthdayService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly GreetingSender _greetingSender;

    public BirthdayService(IEmployeesRepository employeesRepository, GreetingSender greetingSender)
    {
        _employeesRepository = employeesRepository;
        _greetingSender = greetingSender;
    }

    public void SendGreetings(OurDate date)
    {
        _greetingSender.Send(GreetingMessagesFor(EmployeesHavingBirthday(date)));
    }

    private static List<GreetingMessage> GreetingMessagesFor(IEnumerable<Employee> employees)
    {
        return GreetingMessage.GenerateForSome(employees);
    }

    private IEnumerable<Employee> EmployeesHavingBirthday(OurDate today)
    {
        return _employeesRepository.GetAll()
            .FindAll(employee => employee.IsBirthday(today));
    }
}
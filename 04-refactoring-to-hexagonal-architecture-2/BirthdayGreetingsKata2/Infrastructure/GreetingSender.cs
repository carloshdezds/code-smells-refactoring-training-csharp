using System.Collections.Generic;
using BirthdayGreetingsKata2.Core;

namespace BirthdayGreetingsKata2.Application;

public interface GreetingSender
{
    void Send(List<GreetingMessage> messages);
}
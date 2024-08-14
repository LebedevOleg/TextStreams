using System.Net.Http.Json;
using Client.Application;
using Microsoft.Extensions.Configuration;
using TextStreams.Api.Contracts.Enums;

Console.WriteLine("Добро пожаловать в TextStreams!\n");

var builder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

SignalRClient signalRClient = new SignalRClient(builder.Build());
RestClient restClient = new RestClient(builder.Build());

bool isExit = true;

while (isExit)
{
    var command = "";
    if (signalRClient.IsConnected)
    {
        Console.WriteLine("exit - выход с текущей трансляции;");
        command = Console.ReadLine();
        if (command == "exit")
        {
            signalRClient.Leave();
        }
        else
        {
            continue;
        }
    }

    Console.WriteLine("Введите интересующую дату:");
    if (!DateOnly.TryParse(Console.ReadLine(), out var date))
    {
        Console.WriteLine("Некорректная дата");
        continue;
    }

    Console.WriteLine("Получение списка доступных трансляций. . .");

    var groups = await restClient.GetStreams(date);

    Console.SetCursorPosition(0, Console.CursorTop - 1);
    Console.WriteLine("Список доступных трансляций:\n");
    if (!groups.Any())
    {
        Console.WriteLine("Нет доступных трансляций");
    }

    foreach (var group in groups)
    {
        Console.Write($"{groups.IndexOf(group) + 1}. {group.Name} {group.GoalsHome}:{group.GoalsAway}");
        if (group.Status == StreamStatus.End || group.Status == StreamStatus.Announce)
            Console.WriteLine("(нельзя подключиться)");
        else
            Console.WriteLine("(можно подключиться)");
    }

    Console.WriteLine(
        "exit - выход; " +
        "refresh - обновить список; " +
        "1-9 - выбрать трансляцию");

    command = Console.ReadLine();
    switch (command)
    {
        case "exit":
            isExit = false;
            break;
        case "refresh":
            continue;
        default:
            if (!int.TryParse(command, out var index))
            {
                Console.WriteLine("Неверный ввод");
                continue;
            }

            if (index < 1 || index > groups.Count)
            {
                Console.WriteLine("Неверный ввод");
                continue;
            }

            if (groups[index - 1].Status == StreamStatus.End || groups[index - 1].Status == StreamStatus.Announce)
            {
                Console.WriteLine("К трансляции нет доступа");
                continue;
            }

            await signalRClient.Start(groups[index - 1].Name, groups[index - 1].Id);

            break;
    }
}
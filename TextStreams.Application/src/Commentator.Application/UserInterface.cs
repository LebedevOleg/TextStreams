using Microsoft.Extensions.Configuration;
using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;

namespace Commentator.Application;

/// <summary>
/// Пользовательский интерфейс.
/// </summary>
public class UserInterface
{
    private readonly SignalRClient _signalRClient;
    private readonly RestClient _restClient;
    private Stream? _currentStream = null;

    public UserInterface(IConfiguration config)
    {
        var host = config.GetSection("Host").Value;
        if (string.IsNullOrEmpty(host))
            throw new ArgumentException("Host not found in appsettings.json");

        _signalRClient = new SignalRClient(host);
        _restClient = new RestClient(host);
    }

    /// <summary>
    /// Главное меню.
    /// </summary>
    public async Task MainMenu()
    {
        bool isExit = true;
        Console.WriteLine("Добро пожаловать в TextStreams!\n");
        while (isExit)
        {
            Console.WriteLine(
                "exit - выход; " +
                "create - создать новую трансляцию; " +
                "groups - список трансляций; ");

            var command = Console.ReadLine();
            switch (command)
            {
                case "exit":
                    isExit = false;
                    break;
                case "create":
                    Task createStream = CreateStream();
                    Task.WaitAny(createStream);
                    continue;
                case "groups":
                    Console.WriteLine("Введите интересующую дату:");
                    if (!DateOnly.TryParse(Console.ReadLine(), out var date))
                    {
                        Console.WriteLine("Некорректная дата");
                        continue;
                    }

                    Console.WriteLine("Получение списка доступных трансляций. . .");

                    var groups = await _restClient.GetStreams(date);

                    Console.WriteLine("Список доступных трансляций:");
                    if (!groups.Any())
                    {
                        Console.WriteLine("Нет доступных трансляций");
                    }

                    foreach (var group in groups)
                    {
                        Console.Write($"{groups.IndexOf(group) + 1}. {group.Name} ");
                        if (group.Status == StreamStatus.End)
                            Console.WriteLine("(нельзя подключиться)");
                    }

                    Console.WriteLine("\nВыберите трансляцию:");

                    command = Console.ReadLine();
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

                    if (groups[index - 1].Status == StreamStatus.End)
                    {
                        Console.WriteLine("Трансляция уже завершена");
                        continue;
                    }

                    Stream stream = new Stream(_restClient, groups[index - 1]);
                    _currentStream = stream;
                    await _signalRClient.Start(stream);
                    StreamMenu().Wait();

                    break;
                default:
                    continue;
            }
        }
    }

    /// <summary>
    /// Создание стрима.
    /// </summary>
    private Task CreateStream()
    {
        Console.WriteLine("Введите домашнюю команду:");
        var teamHome = Console.ReadLine();
        Console.WriteLine("Введите гостевую команду:");
        var teamAway = Console.ReadLine();
        Console.WriteLine("Введите дату начала:");

        if (!DateTime.TryParse(Console.ReadLine(), out var startTime))
        {
            Console.WriteLine("Некорректная дата");
            return Task.CompletedTask;
        }

        if (string.IsNullOrWhiteSpace(teamAway) || string.IsNullOrWhiteSpace(teamHome))
        {
            Console.WriteLine("Неверный ввод");
            return Task.CompletedTask;
        }

        var request = new StreamRequest()
            { TeamAway = teamAway, TeamHome = teamHome, StartTime = startTime.ToUniversalTime() };

        _restClient.CreateStream(request).Wait();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Меню стрима.
    /// </summary>
    private Task StreamMenu()
    {
        bool isExit = true;

        while (isExit)
        {
            if (_currentStream.Status == StreamStatus.Announce)
                Console.Write("online - начать трансляцию; ");

            if (_currentStream.Status == StreamStatus.Live)
                Console.Write("fh - начать первую половину; ");

            Console.WriteLine(
                "exit - выход; " +
                "+H - добавить гол; " +
                "-H - отменить гол" +
                "+A - добавить гол; " +
                "-A - отменить гол");

            if (_currentStream.Status != StreamStatus.Announce || _currentStream.Status != StreamStatus.End)
                Console.Write(
                    "send - отправить сообщение; ");

            if (_currentStream.Status == StreamStatus.SecondHalf ||
                _currentStream.Status == StreamStatus.SecondOvertime)
                Console.Write("end - завершить трансляцию; ");

            if (_currentStream.Status == StreamStatus.FirstHalf)
                Console.Write("ps - отметка о начале перерыва; ");

            if (_currentStream.Status == StreamStatus.HalfTimeBreak)
                Console.Write("pe - отметка о конце перерыва");

            if (_currentStream.Status == StreamStatus.SecondHalf)
                Console.Write("fo - начать первый овертайм");

            if (_currentStream.Status == StreamStatus.FirstOvertime)
                Console.Write("so - начать второй овертайм \n");

            var command = Console.ReadLine();
            switch (command)
            {
                case "online":
                    if (_currentStream.Status == StreamStatus.Announce)
                    {
                        _currentStream.Status = StreamStatus.Live;
                        _restClient.ChangeStatus(_currentStream).Wait();
                    }

                    break;
                case "fh":
                    if (_currentStream.Status == StreamStatus.Live)
                        _currentStream.StartGame().Wait();
                    break;
                case "fo":
                    if (_currentStream.Status == StreamStatus.SecondHalf)
                        _currentStream.StartFirstOverTime().Wait();
                    break;
                case "so":
                    if (_currentStream.Status == StreamStatus.FirstOvertime)
                        _currentStream.StartSecondOverTime().Wait();
                    break;
                case "end":
                    if (_currentStream.Status == StreamStatus.SecondHalf ||
                        _currentStream.Status == StreamStatus.SecondOvertime)
                    {
                        _currentStream.EndGame().Wait();
                        _currentStream = null;
                        isExit = false;
                    }

                    break;
                case "exit":
                    _signalRClient.Leave(_currentStream);
                    _currentStream = null;
                    isExit = false;
                    break;
                case "send":
                    if (_currentStream.Status == StreamStatus.Announce)
                        break;

                    var message = _currentStream.CreateMessage();
                    _signalRClient.SendMessage(_currentStream, message);
                    break;
                case "+H":
                    _currentStream.ChangeGoal(true).Wait();
                    break;
                case "+A":
                    _currentStream.ChangeGoal(false).Wait();
                    break;
                case "-H":
                    _currentStream.ChangeGoal(true, false).Wait();
                    break;
                case "-A":
                    _currentStream.ChangeGoal(false, false).Wait();
                    break;
                case "ps":
                    if (_currentStream.Status != StreamStatus.FirstHalf)
                        break;

                    _currentStream.HalfTimeStart().Wait();
                    break;
                case "pe":
                    if (_currentStream.Status != StreamStatus.HalfTimeBreak)
                        break;

                    _currentStream.HalfTimeEnd().Wait();
                    break;
            }
        }

        return Task.CompletedTask;
    }
}
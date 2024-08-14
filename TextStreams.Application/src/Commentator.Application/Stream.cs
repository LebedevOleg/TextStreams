using TextStreams.Api.Contracts.Dto;
using TextStreams.Api.Contracts.Enums;

namespace Commentator.Application;

/// <summary>
/// Объект стрима.
/// </summary>
public class Stream
{
    /// <summary>
    /// Текущая группа.
    /// </summary>
    public string CurrentGroup = "";

    /// <summary>
    /// Идентификатор стрима.
    /// </summary>
    public long CurrentGroupId = 0;

    /// <summary>
    /// Название домашней команды.
    /// </summary>
    public string TeamHome = "";

    /// <summary>
    /// Название гостевой команды.
    /// </summary>
    public string TeamAway = "";

    /// <summary>
    /// Количество голов домашней команды.
    /// </summary>
    public int GoalHome = 0;

    /// <summary>
    /// Количество голов гостевой команды.
    /// </summary>
    public int GoalAway = 0;

    /// <summary>
    /// Статус стрима.
    /// </summary>
    public StreamStatus Status;

    public DateTime StartTime;
    private readonly RestClient _restClient;
    private readonly TimeSpan _oneHalfTime = new TimeSpan(0, 45, 0);
    private readonly TimeSpan _gameTime = new TimeSpan(0, 90, 0);
    private readonly TimeSpan _oneOverTime = new TimeSpan(0, 15, 0);


    public Stream(RestClient restClient, StreamResponse stream)
    {
        _restClient = restClient;
        CurrentGroup = stream.Name;
        CurrentGroupId = stream.Id;
        TeamHome = stream.TeamHome;
        TeamAway = stream.TeamAway;
        GoalHome = stream.GoalsHome;
        GoalAway = stream.GoalsAway;
        Status = stream.Status;
        StartTime = stream.StartMatchTime;
    }


    /// <summary>
    /// Начало игры.
    /// </summary>
    public async Task StartGame()
    {
        StartTime = DateTime.Now;
        Status = StreamStatus.FirstHalf;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Конец игры.
    /// </summary>
    public async Task EndGame()
    {
        Status = StreamStatus.End;
        StartTime = DateTime.Now;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Создание сообщения.
    /// </summary>
    /// <returns> Сообщение.</returns>
    /// <exception cref="ArgumentOutOfRangeException"> Ошибка маппинга, состояния не существует.</exception>
    public string CreateMessage()
    {
        string message = "";
        var time = DateTime.Now - StartTime;
        if (Status == StreamStatus.FirstHalf)
        {
            if (time < _oneHalfTime)
                message += $"{time.Minutes:00}:{time.Seconds:00};";
            else
            {
                var bonusTime = time - _oneHalfTime;
                message += $"{_oneHalfTime.Minutes:00}:{_oneHalfTime.Seconds:00} " +
                           $"+{bonusTime.Minutes:00}:{bonusTime.Seconds:00};";
            }
        }
        else if (Status == StreamStatus.HalfTimeBreak)
        {
            message += "Перерыв;";
        }
        else if (Status == StreamStatus.SecondHalf)
        {
            time = time.Add(_oneHalfTime);

            if (time < _gameTime)
                message += $"{time.Minutes + time.Hours * 60}:{time.Seconds:00};";
            else
            {
                var bonusTime = time - _gameTime;
                message += $"{_gameTime.Minutes + _gameTime.Hours * 60}:{_gameTime.Seconds:00} " +
                           $"+{bonusTime.Minutes:00}:{bonusTime.Seconds:00};";
            }
        }
        else if (Status == StreamStatus.FirstOvertime)
        {
            time = time.Add(_gameTime);

            if (time < (_gameTime + _oneOverTime))

                message += $"{time.Minutes + time.Hours * 60}:{time.Seconds:00};";
            else
            {
                var bonusTime = time - (_gameTime + _oneOverTime);
                message +=
                    $"{(_gameTime + _oneOverTime).Minutes + (_gameTime + _oneOverTime).Hours * 60}:" +
                    $"{(_gameTime + _oneOverTime).Seconds:00}" +
                    $" +{bonusTime.Minutes:00}:{bonusTime.Seconds:00};";
            }
        }
        else if (Status == StreamStatus.SecondOvertime)
        {
            time = time.Add(_gameTime + _oneOverTime);
            if (time < _gameTime + _oneOverTime * 2)
                message += $"{time.Minutes + time.Hours * 60}:{time.Seconds:00};";
            else
            {
                var bonusTime = time - _gameTime + _oneOverTime * 2;
                message +=
                    $"{(_gameTime + _oneOverTime * 2).Minutes + (_gameTime + _oneOverTime * 2).Hours * 60}:" +
                    $"{(_gameTime + _oneOverTime * 2).Seconds:00}" +
                    $" +{bonusTime.Minutes:00}:{bonusTime.Seconds:00};";
            }
        }

        Console.WriteLine(
            $"События: \n1.{MatchEvents.YellowCard.Map()}," +
            $"\n 2.{MatchEvents.RedCard.Map()}," +
            $"\n 3.{MatchEvents.Goal.Map()}," +
            $"\n 4.{MatchEvents.OverTime.Map()}," +
            $"\n 5.{MatchEvents.HalfTime.Map()}," +
            $"\n 6.{MatchEvents.Swap.Map()}," +
            $"\n 7.{MatchEvents.VAR.Map()}," +
            $"\n 8.{MatchEvents.Foul.Map()}, " +
            $"\n 9.{MatchEvents.End.Map()}, " +
            $"\n 10. Собственное событие");
        Console.Write("Введите/Выберите событие:");
        var command = Console.ReadLine();
        switch (command)
        {
            case "1":
                message += $"{MatchEvents.YellowCard.Map()};";
                break;
            case "2":
                message += $"{MatchEvents.RedCard.Map()};";
                break;
            case "3":
                Console.WriteLine($"Выберите команду: 1.{TeamHome}, 2.{TeamAway}");
                command = Console.ReadLine();
                ChangeGoal(command.Equals("1"));
                message += $"{MatchEvents.Goal.Map()};{GoalHome}:{GoalAway};";
                break;
            case "4":
                message += $"{MatchEvents.OverTime.Map()};{GoalHome}:{GoalAway};";
                break;
            case "5":
                message += $"{MatchEvents.HalfTime.Map()};{GoalHome}:{GoalAway};";
                break;
            case "6":
                message += $"{MatchEvents.Swap.Map()};";
                break;
            case "7":
                message += $"{MatchEvents.VAR.Map()};";
                break;
            case "8":
                message += $"{MatchEvents.Foul.Map()};";
                break;
            case "9":
                message += $"{MatchEvents.End.Map()};{GoalHome}:{GoalAway};";
                break;
            case "10":
                Console.WriteLine("Введите Ваше собственное событие:");
                message += $"{Console.ReadLine()};";
                break;
            default:
                Console.WriteLine("Неверная команда. Введите Ваше собственное событие:");
                message += $"{Console.ReadLine()};";
                break;
        }

        Console.WriteLine("Введите текст сообщения:");
        message += $"{Console.ReadLine()};";
        return message;
    }

    /// <summary>
    /// Начало первого перерыва.
    /// </summary>
    public async Task HalfTimeStart()
    {
        Status = StreamStatus.HalfTimeBreak;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Конец первого перерыва.
    /// </summary>
    public async Task HalfTimeEnd()
    {
        StartTime = DateTime.Now;
        Status = StreamStatus.SecondHalf;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Начало первого овертайма.
    /// </summary>
    public async Task StartFirstOverTime()
    {
        StartTime = DateTime.Now;
        Status = StreamStatus.FirstOvertime;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Конец первого овертайма.
    /// </summary>
    public async Task StartSecondOverTime()
    {
        StartTime = DateTime.Now;
        Status = StreamStatus.SecondOvertime;
        await _restClient.ChangeStatus(this);
    }

    /// <summary>
    /// Изменение количества голов.
    /// </summary>
    /// <param name="isHome"> Домашняя ли команда.</param>
    /// <param name="isPlus"> В плюс или в минус.</param>
    public async Task ChangeGoal(bool isHome, bool isPlus = true)
    {
        if (isHome)
            GoalHome = isPlus ? GoalHome + 1 : GoalHome - 1;
        else
            GoalAway = isPlus ? GoalAway + 1 : GoalAway - 1;

        await _restClient.ChangeGoal(this);
    }
}
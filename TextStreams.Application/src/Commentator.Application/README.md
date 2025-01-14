# Приложение для комментатора

## Пользовательская инструкция

### Главное меню

> При запуске приложения, перед нами будет главное меню с несколькими выборами действий:
> - exit - выход
>> Закрытие приложения
> - create - создать новую трансляцию
>> Переход в [меню создания трансляции](#создание-трансляции)
> - groups - список трансляций
>> Переход в [меню поиска трансляций](#поиск-трансляции)

### Создание трансляции

> Необходимо согласно подсказкам в коносли ввести название домашней и гостевой команд.
> Затем указать дату и время матча в формате *dd.mm.yyyy HH:MM*.

### Поиск трансляции

> Согласно подсказке в коносли, необходимо ввести дату, на которую необходимо провести поиск
> в формате dd.mm.yyyy
>
> Далее произояйдет запрос на сервер и ответом вернется список трансляций на введенную дату.
>
> Трансляции будут выведены и пронумерованы. Подключиться можно к трансляциям, которые не имеют статус "завершена".
> Чтобы подключиться к трансляции, нужно ввести цифру, которой она пронумерована.
>
> После выбора трансляции произойдет переход в [меню ведения трансляции](#ведение-трансляции).

### Ведение трансляции

> В этом меню есть несколько команд:
> - exit - выход
>> Закрытие приложения
> - online - начать трансляци.
>> Будет разрешено подключение пользователей. Но эта команда не начнет отсчет времени начала матча.
> Это нужно для того, чтобы можно было начать трансляцию до начала матча с целью дать пользователям какие-то вводные,
> если на то есть желание комментатора.
> - fh - начать первый тайм
>> Начинает отсчет времени начала игры. С этого момента считается, что игра началась (пошло игровое время).
> С этого момента при генерации сообщения, в начале будет добавляться метка времени.
> - ps - отметка о начале перерыва между таймами
>> Меняет статус трансляции на перерыв. В этом состоянии вместо метки времени, добавляется слово "Перерыв".
> - pe - отметка о окончании перерыва между таймами
>> Сигнализирует об окончании перрыва и начале второго тайма. В этот момент начинается отсчет времени второго тайма.
> - fo - отметка о начале первого овертайма.
>> Сигнализирует о начале первого овертайма. В этот момент начинается отсчет времени игры первого овертайма.
>>> Поскольку ситуация с овертаймами не является частой, и времени на перерыв сильно меньше,
> сигнала о дополнительном перерыве нет.
> - so - отметка о начале второго овертайма.
>> Сигнализирует о начале второго овертайма. В этот момент начинается отсчет времени игры второго овертайма.
> - end - завершить трансляцию
>> Завершает трансляцию. После чего пользователь не будет иметь возможности подключиться.
> - +H и -H - Отметки для увеличения и уменьшения счета домашней команды.
> - +A и -A - Отметки для увеличения и уменьшения счета гостевой команды.
>> Эти команды введены, для технических моментов, когда гол засчитывают или не засчитывают.
> - send - команда, которая переводит в [меню создания сообщения](#создание-сообщения).

### Создание сообщения

> В этом меню последовательно создается сообщение. Метка времени (если она нужна) ставится автоматически.
>
> Сначала нужно выбрать событие. Которое произошло в матче и в зависимости от события будут предложены дальнейшие
> действия.
> События (за исключением Гола) не несут в себе дополнительной нагрузки, только добавляют текст сообщения.
> Вот список событий:
> - Желтая карточка
> - Красная карточка
> - Первый овертайм
> - Второй овертайм
> > Собатия овертаймов так же добавляют текущий счет.
> - Замена
> - Фол
> - Гол
> > После выбора этого события, согласно подсказкам нужно указать, какая команда забила. Счет изменится автоматически.
> - VAR
> - Конец трансляции
> > Так же добавляется текущий счет.
> > Статус трансляции не изменится.
> - Свое событие
> > Если на поле произошло нечто, не предусмотренное системой, то далее нужно ввести наименование события.
>
> Далее вводится само сообщение. 
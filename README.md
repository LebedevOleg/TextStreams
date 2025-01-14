# Текстовые трансляции

## Задание:

> ### Комментатор
> 1. Зарегистрировать трансляцию. <br/>
     > При регистрации комментатор задает:
> - команду, которая играет дома;
> - команду, которая играет в гостях;
> - дату и время начала трансляции.
> <p> 2. Запустить (начать) трансляцию.
> <br/>После запуска, трансляция становится доступной болельщикам для подключения.
> <p> 3. Отправить сообщение о произошедшем в матче событии.
> <p> 4. Завершить трансляцию.
> <br/> После завершения, трансляция более не доступна для подключения. 

> ### Болельщик
> 1. Получить список трансляций на заданную дату.
     > Болельщик должен "видеть":
> - команду, которая играет дома;
> - команду, которая играет в гостях;
> - статус трансляции (окончен; не начался; тайм и минуту, если трансляция началась).
> 2. Подключиться к трансляции.
     > <br> Подключение возможно только к запущенной комментатором трансляции.
> 3. Отключится от трансляции.

## Пояснения

> Все проекты
> находятся [здесь](https://github.com/digital-competencies-school/oalebedev/tree/finalTask/final-task/TextStreams.Application/src).
> Проект тестов [тут](https://github.com/digital-competencies-school/oalebedev/tree/finalTask/final-task/TextStreams.Application/tests/TextStreams.UnitTests).
>> #### Структура проектов
>> В папке server находятся все проекты, которые связаны с сервером, в том числе и проект миграций.
> В папках Client.Application и Commentator.Application находятся консольные приложения для Клиента и комментатора соответсвенно
> 
> В docker-compose помещено развертывание серверной части.
> В случае если будут изменяться порты, их необходимо поменять и в консольных приложениях в файлах appsettings.json

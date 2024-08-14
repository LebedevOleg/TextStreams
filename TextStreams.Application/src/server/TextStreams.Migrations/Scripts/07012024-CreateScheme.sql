create schema if not exists text_streams;

create table if not exists text_streams.stream
(
    id         bigserial primary key,
    name       varchar(255),
    team_home  varchar(100),
    team_away  varchar(100),
    goals_home int,
    goals_away int,
    start_time timestamp,
    status     varchar(30)
);

create table if not exists text_streams.stream_message
(
    id        bigserial primary key,
    stream_id bigint
        constraint fk_stream references text_streams.stream (Id)
            on delete cascade,
    message   text
);
alter table text_streams.stream
    add column IF NOT EXISTS start_match_time timestamp with time zone;

CREATE TABLE ms_user (
    user_id BIGINT PRIMARY KEY,
    user_name varchar(20),
    password varchar(50),
    is_active bit
);
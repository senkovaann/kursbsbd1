create table UserInfo
(
    UserId integer serial PRIMARY KEY NOT NULL,
 
     Name varchar(255) NOT NULL,

    Password varchar(255) NOT NULL
);

CREATE TABLE UserSession
(
    SessionId serial PRIMARY KEY NOT NULL,
	UserId integer,
	LoginTime timestamp not null
);

ALTER TABLE UserInfo ADD CONSTRAINT Unique_Name UNIQUE (Name);
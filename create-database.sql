drop table if exists Votes;
drop table if exists Elections;
drop table if exists Users;
drop table if exists Ballots;
drop table if exists Candidates;

create table Users(
  userId int primary key identity not null, 
  email varchar(100) unique not null,
  password varchar(100) not null,
  firstName varchar(100) not null,
  lastName varchar(100) not null,
  dateOfBirth date,
  isVerified bit default 0,
  affiliation varchar(100)
);

create table Ballots(
  ballotId int primary key identity not null,
  startDate date not null,
  endDate date not null
);

create table Candidates(
  candidateId int primary key identity not null,
  age int not null,
  description varchar(100) not null,
  policy1 varchar(100) not null,
  policy2 varchar(100) not null,
  firstName varchar(20) not null,
  lastName varchar(20) not null,
  party varchar(20) not null,
  isIncumbent bit default 0  
);

create table Elections(
  electionId int primary key identity not null,
  ballotId int not null,
  candidate1Id int not null,
  candidate2Id int not null,
  foreign key (ballotId) references Ballots (ballotId),
  foreign key (candidate1Id) references Candidates (candidateId),
  foreign key (candidate2Id) references Candidates (candidateId),
  position varchar(20) not null,
  candidate1VoteCount int not null default 0,
  candidate2VoteCount int not null default 0
);

create table Votes(
  VoteId int primary key identity not null,
  electionId int not null,
  candidateId int not null,
  userId int not null,
  foreign key (electionId) references Elections (electionId),
  foreign key (candidateId) references Candidates (candidateId),
  foreign key (userId) references Users (userId),
  dateVoted date
);
CREATE DATABASE EduConnectPruebas;

-- Create Departments table
CREATE TABLE Departments (
    DepartmentID VARCHAR(20) PRIMARY KEY,
    DepartmentName VARCHAR(100)
);

-- Create Cities table
CREATE TABLE Cities (
    CityID VARCHAR(20) PRIMARY KEY,
    CityName VARCHAR(100),
    DepartmentID VARCHAR(20) FOREIGN KEY REFERENCES Departments(DepartmentID)
);

-- Create Colleges table
CREATE TABLE Colleges (
    CollegeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name VARCHAR(100),
    Address VARCHAR(100),
    Latitude VARCHAR(50),
    Longitude VARCHAR(50),
    AdditionalInfo VARCHAR(MAX),
    AvailableSlots INT,
    CityID VARCHAR(20) FOREIGN KEY REFERENCES Cities(CityID)
);

-- Create Roles table
CREATE TABLE Roles (
    RoleID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RoleName VARCHAR(50)
);

-- Create Users table
CREATE TABLE Users (
    UserID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Password VARCHAR(100),
    RoleID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Roles(RoleID),
    Photo VARCHAR(MAX),
    CollegeID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Colleges(CollegeID)
);

-- Create Requests table
CREATE TABLE Requests (
    RequestID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Colleges(CollegeID),
    Status VARCHAR(50)
);

-- Create Matches table
CREATE TABLE Matches (
    MatchID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RequestID_User1 UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Requests(RequestID),
    RequestID_User2 UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Requests(RequestID),
    CreatedDate DATETIME
);

-- Create Chats table
CREATE TABLE Chats (
    ChatID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MatchID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Matches(MatchID),
    Message VARCHAR(MAX),
    SentDate DATETIME,
    Sender VARCHAR(50)
);

-- Create History table
CREATE TABLE History (
    HistoryID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Colleges(CollegeID),
    ChangeType VARCHAR(50),
    ChangeDate DATETIME
);
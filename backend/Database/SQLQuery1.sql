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
    DepartmentID VARCHAR(20) REFERENCES Departments(DepartmentID)
);

-- Create Colleges table
CREATE TABLE Colleges (
    CollegeID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    Name VARCHAR(100),
    Address VARCHAR(100),
    Latitude DECIMAL(9, 6),
    Longitude DECIMAL(9, 6),
    AdditionalInfo VARCHAR(max),
    AvailableSlots INT,
    CityID VARCHAR(20) REFERENCES Cities(CityID)
);

-- Create Roles table
CREATE TABLE Roles (
    RoleID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    RoleName VARCHAR(50)
);

-- Create Users table
CREATE TABLE Users (
    UserID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    Name VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Password VARCHAR(100),
    RoleID UNIQUEIDENTIFIER REFERENCES Roles(RoleID),
    Photo VARCHAR(max),
    CollegeID UNIQUEIDENTIFIER REFERENCES Colleges(CollegeID)
);

-- Create Requests table
CREATE TABLE Requests (
    RequestID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    UserID UNIQUEIDENTIFIER REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER REFERENCES Colleges(CollegeID),
    CreatedDate DATETIME,
    Status VARCHAR(50)
);

-- Create History table
CREATE TABLE History (
    HistoryID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    UserID UNIQUEIDENTIFIER REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER REFERENCES Colleges(CollegeID),
    ChangeType VARCHAR(50),
    ChangeDate DATETIME
);

-- Create Chats table
CREATE TABLE Chats (
    ChatID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    RequestID1 UNIQUEIDENTIFIER REFERENCES Requests(RequestID),
    RequestID2 UNIQUEIDENTIFIER REFERENCES Requests(RequestID),
    CreatedDate DATETIME
);

-- Create ChatMessages table
CREATE TABLE ChatMessages (
    MessageID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT (NEWID()),
    ChatID UNIQUEIDENTIFIER REFERENCES Chats(ChatID),
    SenderID UNIQUEIDENTIFIER REFERENCES Users(UserID),
    Message VARCHAR(max),
    SentDate DATETIME
);


Scaffold-DbContext 'Data Source=REVISION-PC; Initial Catalog=EduConnectPruebas; Integrated Security=true; TrustServerCertificate=true;' Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataContext -Force
Scaffold-DbContext 'Data Source=SQL5106.site4now.net;Initial Catalog=db_a9c67f_educonnect;User Id=db_a9c67f_educonnect_admin;Password=#@Totoro9623' Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataContext -Force



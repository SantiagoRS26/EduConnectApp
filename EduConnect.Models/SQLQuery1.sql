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
    Latitude DECIMAL(9, 6),
    Longitude DECIMAL(9, 6),
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

CREATE TABLE Connections (
    ConnectionID VARCHAR(100) PRIMARY KEY,
    UserID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID)
);

-- Create Requests table
CREATE TABLE Requests (
    RequestID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Colleges(CollegeID),
    CreatedDate DATETIME,
    Status VARCHAR(50)
);

-- Create Chats table
CREATE TABLE Chats (
    ChatID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    RequestID1 UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Requests(RequestID),
    RequestID2 UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Requests(RequestID),
    CreatedDate DATETIME
);

-- Create ChatMensajes table
CREATE TABLE ChatMessages (
    MessageID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ChatID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Chats(ChatID),
    SenderID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID),
    Message VARCHAR(MAX),
    SentDate DATETIME
);

-- Create History table
CREATE TABLE History (
    HistoryID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(UserID),
    CollegeID UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Colleges(CollegeID),
    ChangeType VARCHAR(50),
    ChangeDate DATETIME
);

Scaffold-DbContext "Data Source=REVISION-PC; Initial Catalog=EduConnectPruebas; Integrated Security=true; TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataContext -Force

eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzYW50aWFnb3JzMjYxMUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJ1c2VyIiwiZXhwIjoxNjg2ODUzMTc2LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDU3IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA1NyJ9.9-Apw-99B99CZNYpz3uLwfaAI6ig1efHcEchI140rHM

create database CompanyDB;
USE CompanyDB;


CREATE TABLE Employee (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(MAX),
    FirstName VARCHAR(MAX) NOT NULL,
    LastName VARCHAR(MAX) NOT NULL,
    Phone VARCHAR(MAX),
    Email VARCHAR(MAX)
);


CREATE TABLE Company (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Name VARCHAR(MAX) NOT NULL,
    DirectorId INT,
    CONSTRAINT FK_Company_Employee_DirectorId FOREIGN KEY (DirectorId) REFERENCES Employee(Id) ON DELETE SET NULL
);


CREATE TABLE Division (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Name VARCHAR(MAX) NOT NULL,
    LeaderId INT,
    CompanyId INT NOT NULL,
    CONSTRAINT FK_Division_Employee_LeaderId FOREIGN KEY (LeaderId) REFERENCES Employee(Id) ON DELETE SET NULL,
    CONSTRAINT FK_Division_Company_CompanyId FOREIGN KEY (CompanyId) REFERENCES Company(Id) ON DELETE CASCADE
);


CREATE TABLE Project (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Name VARCHAR(MAX) NOT NULL,
    LeaderId INT,
    DivisionId INT NOT NULL,
    CONSTRAINT FK_Project_Employee_LeaderId FOREIGN KEY (LeaderId) REFERENCES Employee(Id) ON DELETE SET NULL,
    CONSTRAINT FK_Project_Division_DivisionId FOREIGN KEY (DivisionId) REFERENCES Division(Id) ON DELETE CASCADE
);

CREATE TABLE Department (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code VARCHAR(10) NOT NULL UNIQUE,
    Name VARCHAR(MAX) NOT NULL,
    LeaderId INT,
    ProjectId INT NOT NULL,
    CONSTRAINT FK_Department_Employee_LeaderId FOREIGN KEY (LeaderId) REFERENCES Employee(Id) ON DELETE SET NULL,
    CONSTRAINT FK_Department_Project_ProjectId FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Company_DirectorId ON Company (DirectorId);
CREATE INDEX IX_Department_LeaderId ON Department (LeaderId);
CREATE INDEX IX_Department_ProjectId ON Department (ProjectId);
CREATE INDEX IX_Division_CompanyId ON Division (CompanyId);
CREATE INDEX IX_Division_LeaderId ON Division (LeaderId);
CREATE INDEX IX_Project_DivisionId ON Project (DivisionId);
CREATE INDEX IX_Project_LeaderId ON Project (LeaderId);


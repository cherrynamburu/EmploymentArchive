CREATE DATABASE EmploymentArchive


USE EmploymentArchive

CREATE TABLE EmploymentHistory
(
	EmploymentHistoryId INT IDENTITY(1,1) CONSTRAINT PK_EmploymentHistory_EmploymentHistoryId PRIMARY KEY,
	EmploymentStart date NOT NULL,
	EmploymentEnd date NOT NULL
);

CREATE TABLE Job
(
	JobId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Job_JobId PRIMARY KEY,
	JobTitle varchar(255) NOT NULL,
	Employer varchar(255) NOT NULL,
	Description varchar(255) NULL,
	EmploymentHistoryId int NOT NULL CONSTRAINT FK_Job_EmploymentHistoryId FOREIGN KEY REFERENCES EmploymentHistory (EmploymentHistoryId) ON DELETE CASCADE
);

SELECT * FROM EmploymentHistory;

SELECT * FROM Job;


Insert into EmploymentHistory values(CONVERT(date,'" +  EmploymentHistoryModel.From + "', 103),CONVERT(date,'" + EmploymentHistoryModel.To + "',103)

DELETE FROM EmploymentHistory WHERE EmploymentHistory.EmploymentHistoryId = 6;

SELECT * from Job WHERE JobId = 7;

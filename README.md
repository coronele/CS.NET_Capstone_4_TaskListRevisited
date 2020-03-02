# CS.NET_Capstone_4_TaskListRevisited
Grand Circus C# Capstone 4 - Task List Revisited - Simple task list in MVC and Identity Framework. With SQL table.


Use the following to create a table for this project in a database called "CPSTN4_TaskListRev"

USE CPSTN4_TaskListRev

CREATE TABLE UserTasks(
ID INT PRIMARY KEY IDENTITY(1,1),
Description NVARCHAR(100),
DueDate DATE,
Complete BIT,
OwnerID NVARCHAR(450) FOREIGN KEY REFERENCES AspNetUsers(Id)
);

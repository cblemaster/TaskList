USE [master]
GO

DECLARE @SQL nvarchar(1000);
IF EXISTS (SELECT 1 FROM sys.databases WHERE [name] = N'tasklist')
BEGIN
    SET @SQL = N'USE [tasklist];

                 ALTER DATABASE tasklist SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE [master];

                 DROP DATABASE [tasklist];';
    EXEC (@SQL);
END;

CREATE DATABASE [tasklist]
GO

USE [tasklist]
GO

CREATE TABLE [dbo].[folders]
(
	[Id]			[int]			IDENTITY(1,1)		NOT NULL,
	[FolderName]	[varchar](100)						NOT NULL,
 
	CONSTRAINT [PK_Folders] PRIMARY KEY ([Id]),
	CONSTRAINT UC_FolderName UNIQUE([FolderName]) 
)
GO

CREATE TABLE [dbo].[tasks](
	[Id]			[int]			IDENTITY(1,1)		NOT NULL,
	[TaskName]		[varchar](100)						NOT NULL,
	[DueDate]		[date]								NULL,
	[RecurrenceId]  [int]								NOT NULL,
	[IsImportant]	[bit]								NOT NULL,
	[IsComplete]	[bit]								NOT NULL,
	[FolderId]		[int]								NOT NULL,
	[Note]			[varchar](255)						NULL,
 
	CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Tasks_Folders] FOREIGN KEY([FolderId]) REFERENCES [dbo].[folders] ([Id])
		ON UPDATE CASCADE
		ON DELETE CASCADE
)
GO

INSERT INTO [folders] ([FolderName])
VALUES				  ('Important'),
					  ('Completed'),
					  ('Recurring'),
					  ('Planned'),
					  ('Tasks');
GO

CREATE PROCEDURE [dbo].[Folders.GetById]
	@id int
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[folders]
	LEFT JOIN [dbo].[tasks] ON ([dbo].[folders].[Id] = [dbo].[tasks].[FolderId])
	WHERE [dbo].[folders].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Folders.GetByFolderName]
	@folderName varchar(100)
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[folders]
	LEFT JOIN [dbo].[tasks] ON ([dbo].[folders].[Id] = [dbo].[tasks].[FolderId])
	WHERE LOWER([dbo].[folders].[FolderName]) = LOWER(@folderName);
GO

CREATE PROCEDURE [dbo].[Folders.GetAll]
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[folders]
	LEFT JOIN [dbo].[tasks] ON ([dbo].[folders].[Id] = [dbo].[tasks].[FolderId]);
GO

CREATE PROCEDURE [dbo].[Folders.Create]
	@folderName varchar(100),
	@folderId int OUTPUT
AS
	INSERT INTO [dbo].[folders] ([dbo].[folders].[FolderName])
	VALUES (@folderName);

	SELECT @folderId = SCOPE_IDENTITY();
GO

CREATE PROCEDURE [dbo].[Folders.Delete]
	@id int
AS
	DELETE FROM [dbo].[folders]
	WHERE [dbo].[folders].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Folders.Update]
	@id int,
	@folderName varchar(100)
AS
	UPDATE [dbo].[folders]
	SET [dbo].[folders].[FolderName] = @folderName
	WHERE [dbo].[folders].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Tasks.GetById]
	@id int
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[tasks]
	INNER JOIN [dbo].[folders] ON ([dbo].[tasks].[FolderId] = [dbo].[folders].[Id])
	WHERE [dbo].[tasks].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Tasks.Create]
	@taskName varchar(100),
	@dueDate date,
	@recurrenceId int,
	@isImportant bit,
	@isComplete bit,
	@folderId int,
	@note varchar(255),
	@taskId int OUTPUT
AS
	INSERT INTO [dbo].[tasks] ([dbo].[tasks].[TaskName], 
							   [dbo].[tasks].[DueDate], 
							   [dbo].[tasks].[RecurrenceId], 
							   [dbo].[tasks].[IsImportant], 
							   [dbo].[tasks].[IsComplete], 
							   [dbo].[tasks].[FolderId], 
							   [dbo].[tasks].[Note])
	VALUES					  ( @taskName, 
	                            @dueDate, 
								@recurrenceId, 
								@isImportant, 
								@isComplete, 
								@folderId, 
								@note);

	SELECT @taskId = SCOPE_IDENTITY();
GO

CREATE PROCEDURE [dbo].[Tasks.Delete]
	@id int
AS
	DELETE FROM [dbo].[tasks]
	WHERE [dbo].[tasks].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Tasks.Update]
	@id int,
	@taskName varchar(100),
	@dueDate date,
	@recurrenceId int,
	@isImportant bit,
	@isComplete bit,
	@folderId int,
	@note varchar(255)
AS
	UPDATE [dbo].[tasks]
	SET [dbo].[tasks].[TaskName] = @taskName,
	    [dbo].[tasks].[DueDate] = @dueDate,
		[dbo].[tasks].[RecurrenceId] = @recurrenceId,
		[dbo].[tasks].[IsImportant] = @isImportant,
		[dbo].[tasks].[IsComplete] = @isComplete,
		[dbo].[tasks].[FolderId] = @folderId,
		[dbo].[tasks].[Note] = @note
	WHERE [dbo].[tasks].[Id] = @id;
GO

CREATE PROCEDURE [dbo].[Tasks.GetImportant]
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[tasks]
	INNER JOIN [dbo].[folders] ON ([dbo].[tasks].[FolderId] = [dbo].[folders].[Id])
	WHERE [dbo].[tasks].[IsImportant] = 1;
GO

CREATE PROCEDURE [dbo].[Tasks.GetCompleted]
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[tasks]
	INNER JOIN [dbo].[folders] ON ([dbo].[tasks].[FolderId] = [dbo].[folders].[Id])
	WHERE [dbo].[tasks].[IsComplete] = 1;
GO

CREATE PROCEDURE [dbo].[Tasks.GetRecurring]
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[tasks]
	INNER JOIN [dbo].[folders] ON ([dbo].[tasks].[FolderId] = [dbo].[folders].[Id])
	WHERE [dbo].[tasks].[RecurrenceId] <> 0;
GO

CREATE PROCEDURE [dbo].[Tasks.GetPlanned]
AS
	SELECT [dbo].[folders].[Id] AS 'FolderId',
		   [dbo].[folders].[FolderName],
		   [dbo].[tasks].[Id] AS 'TaskId',
		   [dbo].[tasks].[TaskName],
		   [dbo].[tasks].[DueDate],
		   [dbo].[tasks].[RecurrenceId],
		   [dbo].[tasks].[IsImportant],
		   [dbo].[tasks].[IsComplete],
		   [dbo].[tasks].[Note]
	FROM [dbo].[tasks]
	INNER JOIN [dbo].[folders] ON ([dbo].[tasks].[FolderId] = [dbo].[folders].[Id])
	WHERE [dbo].[tasks].[DueDate] IS NOT NULL;
GO

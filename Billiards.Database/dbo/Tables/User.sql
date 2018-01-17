CREATE TABLE [dbo].[User] (
    [UserId]    INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (100) NOT NULL,
    [LastName]  VARCHAR (100) NOT NULL,
    [IsActive]  BIT           NOT NULL,
    [Handicap] INT NOT NULL DEFAULT 4, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


CREATE TABLE [dbo].[GameUser] (
    [GameUserId]     INT IDENTITY (1, 1) NOT NULL,
    [GameId]         INT NOT NULL,
    [UserId]         INT NOT NULL,
    [DefensiveShots] INT NOT NULL,
    [Timeouts]       INT NOT NULL,
    [IsActive]       BIT NOT NULL,
    CONSTRAINT [PK_GameUser] PRIMARY KEY CLUSTERED ([GameUserId] ASC),
    CONSTRAINT [FK_GameUser_Game] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game] ([GameId]),
    CONSTRAINT [FK_GameUser_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);


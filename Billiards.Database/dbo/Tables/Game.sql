CREATE TABLE [dbo].[Game] (
    [GameId]       INT IDENTITY (1, 1) NOT NULL,
    [MatchId]      INT NOT NULL,
    [Number]       INT NOT NULL,
    [WinnerUserId] INT NULL,
    [IsActive]     BIT NOT NULL,
    [Innings]      INT NOT NULL,
    CONSTRAINT [PK_GameId] PRIMARY KEY CLUSTERED ([GameId] ASC),
    CONSTRAINT [FK_Game_Match] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Match] ([MatchId]),
    CONSTRAINT [FK_Game_User] FOREIGN KEY ([WinnerUserId]) REFERENCES [dbo].[User] ([UserId])
);








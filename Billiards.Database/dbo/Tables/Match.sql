CREATE TABLE [dbo].[Match] (
    [MatchId]  INT      IDENTITY (1, 1) NOT NULL,
    [Date]     DATETIME NOT NULL,
    [User1Id]  INT      NOT NULL,
    [User2Id]  INT      NOT NULL,
    [IsActive] BIT      NULL,
    [MatchType] INT NOT NULL DEFAULT 1, 
    [User1Points] INT NOT NULL DEFAULT 0, 
    [User2Points] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Match] PRIMARY KEY CLUSTERED ([MatchId] ASC),
    CONSTRAINT [FK_Match_User] FOREIGN KEY ([User1Id]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Match_User1] FOREIGN KEY ([User2Id]) REFERENCES [dbo].[User] ([UserId])
);






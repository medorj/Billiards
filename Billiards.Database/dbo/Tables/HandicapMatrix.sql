CREATE TABLE [dbo].[HandicapMatrix]
(
	[HandicapMatrixId] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Player1] INT NOT NULL, 
    [Player2] INT NOT NULL, 
    [Player1Wins] INT NOT NULL, 
    [Player2Wins] INT NOT NULL
)

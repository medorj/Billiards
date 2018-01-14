CREATE TABLE [dbo].[Login] (
    [LoginId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserName]  VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (20)  NOT NULL,
    [FirstName] VARCHAR (200) NOT NULL,
    [LastName]  VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([LoginId] ASC)
);


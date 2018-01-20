CREATE TABLE [dbo].[Login] (
    [LoginId]   INT           IDENTITY (1, 1) NOT NULL,
    [UserName]  VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (20)  NOT NULL,
    [FirstName] VARCHAR (200) NOT NULL,
    [LastName]  VARCHAR (200) NOT NULL,
    [UserId] INT NULL, 
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([LoginId] ASC), 
    CONSTRAINT [FK_Login_User] FOREIGN KEY (UserId) REFERENCES [User]([UserId])
);


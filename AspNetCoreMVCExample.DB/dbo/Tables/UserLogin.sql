CREATE TABLE [dbo].[UserLogin] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              INT NOT NULL,
	[IsSuppressed]    BIT              CONSTRAINT [DF_UserLogin_IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedDt]       DATETIME2         NOT NULL,
    [CreatedByUserId] INT              NOT NULL,
    [UpdatedDt]       DATETIME2         NULL,
    [UpdatedByUserId] INT              NULL,
    [LastUpdateGuid]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[EntityGuid]			UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_UserLogin_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId]
    ON [dbo].[UserLogin]([UserId] ASC);


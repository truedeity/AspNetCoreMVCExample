CREATE TABLE [dbo].[UserRole] (
	[UserRoleId]	INT NOT NULL IDENTITY(1, 1),
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
	[IsSuppressed]    BIT              CONSTRAINT [DF_UserRole_IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedDt]       DATETIME2         NOT NULL,
    [CreatedByUserId] INT              NOT NULL,
    [UpdatedDt]       DATETIME2         NULL,
    [UpdatedByUserId] INT              NULL,
    [LastUpdateGuid]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[EntityGuid]	  uniqueidentifier not null default(newid()),
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_UserRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId]
    ON [dbo].[UserRole]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserRole_UserId]
    ON [dbo].[UserRole]([UserId] ASC);


CREATE TABLE [dbo].[Role] (
    [RoleId]               INT NOT NULL IDENTITY(1,1),
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
	[IsSuppressed]    BIT              CONSTRAINT [DF_Roles_IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedDt]       DATETIME2         NOT NULL,
    [CreatedByUserId] INT              NOT NULL,
    [UpdatedDt]       DATETIME2         NULL,
    [UpdatedByUserId] INT              NULL,
    [LastUpdateGuid]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[EntityGuid]			UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()),
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[Role]([NormalizedName] ASC);


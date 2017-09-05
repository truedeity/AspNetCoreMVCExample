CREATE TABLE [dbo].[User] (
    [UserId]          INT              IDENTITY (1, 1) NOT NULL,
    
    [AccessFailedCount]    INT                NOT NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,


    [IsSuppressed]    BIT              CONSTRAINT [DF_User_IsSuppressed] DEFAULT ((0)) NOT NULL,
    [CreatedDt]       DATETIME2         NOT NULL,
    [CreatedByUserId] INT              NOT NULL,
    [UpdatedDt]       DATETIME2         NULL,
    [UpdatedByUserId] INT              NULL,
    [LastUpdateGuid]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[EntityGuid]	  uniqueidentifier not null default(newid()),
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


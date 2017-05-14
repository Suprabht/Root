USE [BridgeToCare]
GO

/****** Object: Table [dbo].[AspNetRoleClaims] Script Date: 3/23/2017 8:59:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    [RoleId]     NVARCHAR (450) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId]
    ON [dbo].[AspNetRoleClaims]([RoleId] ASC);


GO
ALTER TABLE [dbo].[AspNetRoleClaims]
    ADD CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[AspNetRoleClaims]
    ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;



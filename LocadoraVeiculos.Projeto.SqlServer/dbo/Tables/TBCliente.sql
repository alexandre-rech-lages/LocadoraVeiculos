﻿CREATE TABLE [dbo].[TBCliente] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Nome]         VARCHAR (300)    NOT NULL,
    [Documento]    VARCHAR (30)     NOT NULL,
    [Email]        VARCHAR (100)    NOT NULL,
    [Telefone]     VARCHAR (30)     NOT NULL,
    [Numero]       INT              NOT NULL,
    [Bairro]       VARCHAR (300)    NOT NULL,
    [Rua]          VARCHAR (300)    NOT NULL,
    [Estado]       VARCHAR (3)      NOT NULL,
    [Cidade]       VARCHAR (300)    NOT NULL,
    [Tipo_cliente] INT              NOT NULL,
    CONSTRAINT [PK_TBCliente] PRIMARY KEY CLUSTERED ([Id] ASC)
);


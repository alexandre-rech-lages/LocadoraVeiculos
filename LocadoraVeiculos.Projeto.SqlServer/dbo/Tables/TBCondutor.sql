CREATE TABLE [dbo].[TBCondutor] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Nome]       VARCHAR (50)     NOT NULL,
    [Cliente_Id] INT              NOT NULL,
    CONSTRAINT [PK_TBCondutor_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


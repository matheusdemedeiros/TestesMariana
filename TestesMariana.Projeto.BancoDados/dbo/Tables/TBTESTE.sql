CREATE TABLE [dbo].[TBTESTE] (
    [Numero]            INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]            VARCHAR (300) NOT NULL,
    [Disciplina_Numero] INT           NOT NULL,
    [Materia_Numero]    INT           NULL,
    [Serie]             VARCHAR (300) NOT NULL,
    [DataCriacao]       DATETIME      NOT NULL,
    CONSTRAINT [PK_TBTESTE] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBTESTE_TBDISCIPLINA] FOREIGN KEY ([Disciplina_Numero]) REFERENCES [dbo].[TBDISCIPLINA] ([Numero]),
    CONSTRAINT [FK_TBTESTE_TBMATERIA] FOREIGN KEY ([Materia_Numero]) REFERENCES [dbo].[TBMATERIA] ([Numero])
);


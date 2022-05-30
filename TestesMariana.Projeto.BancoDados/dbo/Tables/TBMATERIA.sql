CREATE TABLE [dbo].[TBMATERIA] (
    [Numero]            INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]            VARCHAR (300) NOT NULL,
    [Serie]             VARCHAR (300) NOT NULL,
    [Disciplina_Numero] INT           NOT NULL,
    CONSTRAINT [PK_TBMATERIA] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBMATERIA_TBDISCIPLINA] FOREIGN KEY ([Disciplina_Numero]) REFERENCES [dbo].[TBDISCIPLINA] ([Numero])
);


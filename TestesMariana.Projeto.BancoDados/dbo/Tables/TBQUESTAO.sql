CREATE TABLE [dbo].[TBQUESTAO] (
    [Numero]            INT           IDENTITY (1, 1) NOT NULL,
    [Enunciado]         VARCHAR (300) NOT NULL,
    [Materia_Numero]    INT           NOT NULL,
    [Disciplina_Numero] INT           NOT NULL,
    [Serie]             VARCHAR (300) NOT NULL,
    CONSTRAINT [PK_TBQUESTAO] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBQUESTAO_TBDISCIPLINA] FOREIGN KEY ([Disciplina_Numero]) REFERENCES [dbo].[TBDISCIPLINA] ([Numero]),
    CONSTRAINT [FK_TBQUESTAO_TBMATERIA] FOREIGN KEY ([Materia_Numero]) REFERENCES [dbo].[TBMATERIA] ([Numero])
);


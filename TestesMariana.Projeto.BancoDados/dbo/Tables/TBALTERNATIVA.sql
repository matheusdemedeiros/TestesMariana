CREATE TABLE [dbo].[TBALTERNATIVA] (
    [Numero]         INT           IDENTITY (1, 1) NOT NULL,
    [Descricao]      VARCHAR (300) NOT NULL,
    [Correta]        BIT           NOT NULL,
    [Letra]          VARCHAR (2)   NOT NULL,
    [Questao_Numero] INT           NOT NULL,
    CONSTRAINT [PK_TBALTERNATIVA] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBALTERNATIVA_TBQUESTAO] FOREIGN KEY ([Questao_Numero]) REFERENCES [dbo].[TBQUESTAO] ([Numero])
);


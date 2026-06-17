IF DB_ID('ControleDeMedicamentosWeb') IS NULL
BEGIN
    CREATE DATABASE [ControleDeMedicamentosWeb];
END;

USE [ControleDeMedicamentosWeb]
GO

CREATE TABLE [dbo].[TBPaciente] (
[Id] uniqueidentifier NOT NULL,
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(15) NOT NULL,
[Cpf] nvarchar(14) NOT NULL,
[Cartaosus] nvarchar(15) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBMedicamento] (
[Id] uniqueidentifier NOT NULL,
[Nome] nvarchar(100) NOT NULL,
[Descricao] nvarchar(255) NOT NULL,
[FornecedorId] uniqueidentifier NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBFornecedor] (
[Id] uniqueidentifier NOT NULL,
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(15) NOT NULL,
[Cnpj] nvarchar(18) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBFuncionario] (
[Id] uniqueidentifier NOT NULL,
[Nome] nvarchar(100) NOT NULL,
[Telefone] nvarchar(15) NOT NULL,
[Cpf] nvarchar(14) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBRequisicao] (
[Id] uniqueidentifier NOT NULL,
[DataCriacao] datetime2(7) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBRequisicaoEntrada] (
[Id] uniqueidentifier NOT NULL,
[FuncionarioId] uniqueidentifier NOT NULL,
[MedicamentoId] uniqueidentifier NOT NULL,
[Quantidade] int NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBRequisicaoSaida] (
[Id] uniqueidentifier NOT NULL,
[PacienteId] uniqueidentifier NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBMedicamentoPrescrito] (
[RequisicaoSaidaId] uniqueidentifier NOT NULL,
[MedicamentoId] uniqueidentifier NOT NULL,
[Quantidade] int NOT NULL,
PRIMARY KEY ([RequisicaoSaidaId], [MedicamentoId])
);


ALTER TABLE [dbo].[TBMedicamento]
ADD CONSTRAINT [FK_TBMedicamento_TBFornecedor]
FOREIGN KEY ([FornecedorId])
REFERENCES [dbo].[TBFornecedor]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBRequisicaoEntrada]
ADD CONSTRAINT [FK_TBRequisicaoEntrada_TBRequisicao]
FOREIGN KEY ([Id])
REFERENCES [dbo].[TBRequisicao]([Id])
ON DELETE CASCADE
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBRequisicaoEntrada]
ADD CONSTRAINT [FK_TBRequisicaoEntrada_TBFuncionario]
FOREIGN KEY ([FuncionarioId])
REFERENCES [dbo].[TBFuncionario]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBRequisicaoEntrada]
ADD CONSTRAINT [FK_TBRequisicaoEntrada_TBMedicamento]
FOREIGN KEY ([MedicamentoId])
REFERENCES [dbo].[TBMedicamento]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBRequisicaoSaida]
ADD CONSTRAINT [FK_TBRequisicaoSaida_TBRequisicao]
FOREIGN KEY ([Id])
REFERENCES [dbo].[TBRequisicao]([Id])
ON DELETE CASCADE
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBRequisicaoSaida]
ADD CONSTRAINT [FK_TBRequisicaoSaida_TBPaciente]
FOREIGN KEY ([PacienteId])
REFERENCES [dbo].[TBPaciente]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBMedicamentoPrescrito]
ADD CONSTRAINT [FK_TBMedicamentoPrescrito_TBRequisicaoSaida]
FOREIGN KEY ([RequisicaoSaidaId])
REFERENCES [dbo].[TBRequisicaoSaida]([Id])
ON DELETE CASCADE
ON UPDATE NO ACTION;



ALTER TABLE [dbo].[TBMedicamentoPrescrito]
ADD CONSTRAINT [FK_TBMedicamentoPrescrito_TBMedicamento]
FOREIGN KEY ([MedicamentoId])
REFERENCES [dbo].[TBMedicamento]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;




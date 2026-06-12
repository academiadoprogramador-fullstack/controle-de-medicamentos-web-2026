# Controle de Medicamentos

## Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2026

## Funcionalidades

### 1. Módulo de Fornecedores

#### **Requisitos Funcionais**

- O sistema deve permitir registrar novos fornecedores
- O sistema deve permitir visualizar todos os fornecedores cadastrados
- O sistema deve permitir editar fornecedores existentes
- O sistema deve permitir excluir fornecedores cadastrados

#### **Regras de Negócio**

Campos obrigatórios:

- Nome (3-100 caracteres)
- Telefone (formatos válidos)
- CNPJ (14 dígitos)

> O sistema não deve permitir cadastro de fornecedores com mesmo CNPJ

---

### 2. Módulo de Pacientes

#### **Requisitos Funcionais**

- O sistema deve permitir registrar novos pacientes
- O sistema deve permitir visualizar todos os pacientes cadastrados
- O sistema deve permitir editar pacientes existentes
- O sistema deve permitir excluir pacientes cadastrados

#### **Regras de Negócio**

Campos obrigatórios:

- Nome (3-100 caracteres)
- Telefone (formatos válidos: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
- Cartão do SUS (15 dígitos)
- CPF (11 dígitos)

> O sistema não deve permitir cadastro de pacientes com mesmo cartão do SUS

---

### 3. Módulo de Medicamentos

#### **Requisitos Funcionais**

- O sistema deve permitir registrar novos medicamentos
- O sistema deve permitir visualizar todos os medicamentos cadastrados
- O sistema deve permitir editar medicamentos existentes
- O sistema deve permitir excluir medicamentos cadastrados

#### **Regras de Negócio**

Campos obrigatórios:

- Nome (3-100 caracteres)
- Descrição (5-255 caracteres)
- Quantidade em estoque (número positivo)
- Fornecedor

> O sistema deve destacar medicamentos com menos de 20 unidades como "em falta"

> O sistema deve atualizar a quantidade quando o medicamento já estiver cadastrado

---

### 4. Módulo de Funcionários

#### **Requisitos Funcionais**

- O sistema deve permitir registrar novos funcionários
- O sistema deve permitir visualizar todos os funcionários cadastrados
- O sistema deve permitir editar funcionários existentes
- O sistema deve permitir excluir funcionários cadastrados

#### **Regras de Negócio**

Campos obrigatórios:

- Nome (3-100 caracteres)
- Telefone (formatos válidos)
- CPF (11 dígitos)

> O sistema não deve permitir cadastro de funcionários com mesmo CPF

---

### 5. Módulo de Estoque

#### 5.1 Requisições de Entrada

#### **Requisitos Funcionais para Requisições de Entrada**

- O sistema deve permitir registrar novas requisições de entrada
- O sistema deve permitir visualizar todas as requisições de entrada

#### **Regras de Negócio para Requisições de Entrada**

Campos obrigatórios:

- Data (válida)
- Medicamento (seleção obrigatória)
- Funcionário (seleção obrigatória)
- Quantidade (número positivo)

> O sistema deve atualizar o estoque ao registrar a requisição de entrada

---

#### 5.2 Requisições de Saída

#### **Requisitos Funcionais para Requisições de Saída**

- O sistema deve permitir registrar novas requisições de saída
- O sistema deve permitir visualizar todas as requisições de saída

#### **Regras de Negócio para Requisições de Saída**

Campos obrigatórios:

- Data (válida)
- Paciente (seleção obrigatória)
- Medicamentos Requisitados (seleção obrigatória)

> O sistema não deve permitir requisição que exceda o estoque disponível

> O sistema deve subtrair a quantidade do estoque ao registrar a requisição

---

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

   ```bash
   dotnet restore
   ```

4. Para executar o projeto compilando em tempo real

   ```bash
   dotnet run --project ControleDeMedicamentosWeb.WebApp
   ```

## Requisitos

- .NET 10.0 SDK

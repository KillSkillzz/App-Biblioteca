# 📚 Sistema de Gerenciamento de Biblioteca em C#

Este projeto é um sistema simples de biblioteca desenvolvido em **C#**, com foco em conceitos de **POO (Programação Orientada a Objetos)**. Ele permite o gerenciamento de livros, usuários (alunos e professores) e empréstimos.

## 🧠 Conceitos Aplicados

- **Encapsulamento**
- **Herança**
- **Abstração**
- **Composição**
- **Listas e Estruturas de Dados**
- **Interação com o usuário via Console**

## 🚀 Funcionalidades

✅ Cadastro de livros  
✅ Cadastro de usuários (Alunos ou Professores)  
✅ Realização de empréstimos  
✅ Registro de devoluções  
✅ Listagem de livros disponíveis  
✅ Listagem de usuários cadastrados  
✅ Histórico de empréstimos por usuário  
✅ Listagem de livros emprestados atualmente  

## 🏗️ Estrutura do Projeto

- `Livro`: Representa os livros da biblioteca, com controle de disponibilidade.
- `Usuario`: Classe abstrata base para `Aluno` e `Professor`.
- `Aluno` / `Professor`: Herdam de `Usuario`, com atributos específicos.
- `Emprestimo`: Registra o empréstimo de um livro por um usuário, incluindo datas.
- `Biblioteca`: Gerencia os cadastros, empréstimos e consultas.
- `Program`: Contém o `Main` e o menu de interação com o sistema.

## 💻 Como Executar

1. Abra o projeto em um ambiente compatível com C# (.NET), como Visual Studio ou VS Code com o SDK .NET instalado.
2. Compile e execute o projeto.
3. Navegue pelo menu digitando a opção desejada.

`

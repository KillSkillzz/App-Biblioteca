using System;
using System.Collections.Generic;

// essa é classe que representa o livro
class Livro
{
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public int Ano { get; private set; }
    public string ISBN { get; private set; }
    public bool Disponivel { get; private set; }

    public Livro(string titulo, string autor, int ano, string isbn)
    {
        Titulo = titulo;
        Autor = autor;
        Ano = ano;
        ISBN = isbn;
        Disponivel = true;
    }

    public void MarcarComoEmprestado() => Disponivel = false;
    public void MarcarComoDevolvido() => Disponivel = true;

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Título: {Titulo}, Autor: {Autor}, Ano: {Ano}, ISBN: {ISBN}, Status: {(Disponivel ? "Disponível" : "Emprestado")}");
    }
}

//  atributos do livro com encapsulamento (somente leitura publica) 
abstract class Usuario
{
    public string Nome { get; protected set; }
    public string ID { get; protected set; }
    public string Tipo { get; protected set; }

    public Usuario(string nome, string id, string tipo)
    {
        Nome = nome;
        ID = id;
        Tipo = tipo;
    }

    public abstract void ExibirInformacoes();
}

// classe Aluno (herda Usuario)
class Aluno : Usuario
{
    public string Curso { get; private set; }
    public string Matricula { get; private set; }

    public Aluno(string nome, string id, string curso, string matricula)
        : base(nome, id, "Aluno")
    {
        Curso = curso;
        Matricula = matricula;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Aluno: {Nome}, ID: {ID}, Curso: {Curso}, Matrícula: {Matricula}");
    }
}

// classe professor (herda Usuario)
class Professor : Usuario
{
    public string Departamento { get; private set; }
    public string Registro { get; private set; }

    public Professor(string nome, string id, string departamento, string registro)
        : base(nome, id, "Professor")
    {
        Departamento = departamento;
        Registro = registro;
    }

    public override void ExibirInformacoes()
    {
        Console.WriteLine($"Professor: {Nome}, ID: {ID}, Departamento: {Departamento}, Registro: {Registro}");
    }
}

// classe emprestimo (composição de Livro e Usuario)
class Emprestimo
{
    public Usuario Usuario { get; private set; }
    public Livro Livro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime? DataDevolucao { get; private set; }
    public string Status => DataDevolucao == null ? "Ativo" : "Concluído";

    public Emprestimo(Usuario usuario, Livro livro)
    {
        Usuario = usuario;
        Livro = livro;
        DataEmprestimo = DateTime.Now;
        DataDevolucao = null;
        livro.MarcarComoEmprestado();
    }

    public void RegistrarDevolucao()
    {
        DataDevolucao = DateTime.Now;
        Livro.MarcarComoDevolvido();
    }

    public void ExibirResumo()
    {
        Console.WriteLine($"Usuário: {Usuario.Nome}, Livro: {Livro.Titulo}, Empréstimo: {DataEmprestimo}, Devolução: {DataDevolucao}, Status: {Status}");
    }
}

// Sistema Biblioteca
class Biblioteca
{
    private List<Usuario> usuarios = new List<Usuario>();
    private List<Livro> livros = new List<Livro>();
    private List<Emprestimo> emprestimos = new List<Emprestimo>();

    public void CadastrarLivro()
    {
        Console.Write("Título: ");
        string titulo = Console.ReadLine();
        Console.Write("Autor: ");
        string autor = Console.ReadLine();
        Console.Write("Ano: ");
        int ano = int.Parse(Console.ReadLine());
        Console.Write("ISBN: ");
        string isbn = Console.ReadLine();

        livros.Add(new Livro(titulo, autor, ano, isbn));
        Console.WriteLine("Livro cadastrado com sucesso.");
    }

    public void CadastrarUsuario()
    {
        Console.Write("Aluno (1) ou Professor (2)? ");
        string tipo = Console.ReadLine();

        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("ID: ");
        string id = Console.ReadLine();

        if (tipo == "1")
        {
            Console.Write("Curso: ");
            string curso = Console.ReadLine();
            Console.Write("Matrícula: ");
            string matricula = Console.ReadLine();
            usuarios.Add(new Aluno(nome, id, curso, matricula));
        }
        else
        {
            Console.Write("Departamento: ");
            string dep = Console.ReadLine();
            Console.Write("Registro: ");
            string registro = Console.ReadLine();
            usuarios.Add(new Professor(nome, id, dep, registro));
        }

        Console.WriteLine("Usuário cadastrado com sucesso.");
    }

    public void RealizarEmprestimo()
    {
        Console.Write("ID do usuário: ");
        string id = Console.ReadLine();
        Usuario usuario = usuarios.Find(u => u.ID == id);

        if (usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        Console.Write("ISBN do livro: ");
        string isbn = Console.ReadLine();
        Livro livro = livros.Find(l => l.ISBN == isbn && l.Disponivel);

        if (livro == null)
        {
            Console.WriteLine("Livro não disponível.");
            return;
        }

        emprestimos.Add(new Emprestimo(usuario, livro));
        Console.WriteLine("Empréstimo realizado com sucesso.");
    }

    public void RegistrarDevolucao()
    {
        Console.Write("ISBN do livro: ");
        string isbn = Console.ReadLine();
        Emprestimo emp = emprestimos.Find(e => e.Livro.ISBN == isbn && e.DataDevolucao == null);

        if (emp == null)
        {
            Console.WriteLine("Empréstimo ativo não encontrado.");
            return;
        }

        emp.RegistrarDevolucao();
        Console.WriteLine("Devolução registrada.");
    }

    public void ListarLivrosDisponiveis()
    {
        Console.WriteLine("Livros disponíveis:");
        foreach (var livro in livros)
        {
            if (livro.Disponivel)
                livro.ExibirInformacoes();
        }
    }

    public void ListarUsuarios()
    {
        Console.WriteLine("Usuários cadastrados:");
        foreach (var usuario in usuarios)
        {
            usuario.ExibirInformacoes();
        }
    }

    public void HistoricoEmprestimos()
    {
        Console.Write("ID do usuário: ");
        string id = Console.ReadLine();
        foreach (var emp in emprestimos)
        {
            if (emp.Usuario.ID == id)
                emp.ExibirResumo();
        }
    }

    public void LivrosEmprestados()
    {
        Console.WriteLine("Livros atualmente emprestados:");
        foreach (var emp in emprestimos)
        {
            if (emp.DataDevolucao == null)
                emp.ExibirResumo();
        }
    }
}

// Programa Principal (Main)
class Program
{
    static void Main(string[] args)
    {
        Biblioteca biblioteca = new Biblioteca();
        string opcao;

        do //laço while
        {
            Console.WriteLine("\nSistema de Biblioteca");
            Console.WriteLine("1 - Cadastrar Livro");
            Console.WriteLine("2 - Cadastrar Usuário");
            Console.WriteLine("3 - Realizar Empréstimo");
            Console.WriteLine("4 - Registrar Devolução");
            Console.WriteLine("5 - Listar Livros Disponíveis");
            Console.WriteLine("6 - Listar Usuários");
            Console.WriteLine("7 - Histórico de Empréstimos por Usuário");
            Console.WriteLine("8 - Livros Emprestados");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": biblioteca.CadastrarLivro(); break;
                case "2": biblioteca.CadastrarUsuario(); break;
                case "3": biblioteca.RealizarEmprestimo(); break;
                case "4": biblioteca.RegistrarDevolucao(); break;
                case "5": biblioteca.ListarLivrosDisponiveis(); break;
                case "6": biblioteca.ListarUsuarios(); break;
                case "7": biblioteca.HistoricoEmprestimos(); break;
                case "8": biblioteca.LivrosEmprestados(); break;
            }

        } while (opcao != "0");

        Console.WriteLine("Programa encerrado.");
    }
}

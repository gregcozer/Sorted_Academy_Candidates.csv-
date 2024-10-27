using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Candidato
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Estado { get; set; }
    public string Vaga { get; set; }
}

class Program
{
    static void Main()
    {
        List<Candidato> candidatos = new List<Candidato>
        {
            new Candidato { Nome = "Ana", Idade = 25, Estado = "SC", Vaga = "QA" },
            new Candidato { Nome = "Otto", Idade = 16, Estado = "SC", Vaga = "QA" }, 
            new Candidato { Nome = "Eve", Idade = 22, Estado = "SP", Vaga = "Web" },
            new Candidato { Nome = "Bob", Idade = 30, Estado = "RJ", Vaga = "Web" },
            new Candidato { Nome = "Carla", Idade = 34, Estado = "PI", Vaga = "Mobile" }, 
            new Candidato { Nome = "Lucas", Idade = 28, Estado = "MG", Vaga = "Mobile" },
        };
    
        ExibirProporcaoPorVaga(candidatos);
       
         ExibirIdadeMediaQA(candidatos);
       
          ExibirMaisVelhoMobile(candidatos);
        
           ExibirMaisNovoWeb(candidatos);
       
            ExibirSomaIdadesQA(candidatos);
        
             ExibirEstadosDistintos(candidatos);
        
              CriarArquivoCSV(candidatos);
       
               DescobrirInstrutorQA(candidatos);

                DescobrirInstrutorMobile(candidatos);
    }

    static void ExibirProporcaoPorVaga(List<Candidato> candidatos)
    {
        var totalCandidatos = candidatos.Count;
        var vagas = candidatos.GroupBy(c => c.Vaga)
                              .Select(g => new { Vaga = g.Key, Quantidade = g.Count() });

        Console.WriteLine("Proporção de candidatos por vaga (porcentagem):");
        foreach (var vaga in vagas)
        {
            var porcentagem = (vaga.Quantidade / (double)totalCandidatos) * 100;
            Console.WriteLine($"Vaga: {vaga.Vaga}, Porcentagem: {porcentagem:F2}%");
        }
        Console.WriteLine();
    }

    static void ExibirIdadeMediaQA(List<Candidato> candidatos)
    {
        var idadeMediaQA = candidatos.Where(c => c.Vaga == "QA").Average(c => c.Idade);
        Console.WriteLine($"Idade média dos candidatos de QA: {idadeMediaQA:F2} anos\n");
    }

    static void ExibirMaisVelhoMobile(List<Candidato> candidatos)
    {
        var maisVelhoMobile = candidatos.Where(c => c.Vaga == "Mobile").Max(c => c.Idade);
        Console.WriteLine($"Idade do candidato mais velho de Mobile: {maisVelhoMobile} anos\n");
    }

    static void ExibirMaisNovoWeb(List<Candidato> candidatos)
    {
        var maisNovoWeb = candidatos.Where(c => c.Vaga == "Web").Min(c => c.Idade);
        Console.WriteLine($"Idade do candidato mais novo de Web: {maisNovoWeb} anos\n");
    }

    static void ExibirSomaIdadesQA(List<Candidato> candidatos)
    {
        var somaIdadesQA = candidatos.Where(c => c.Vaga == "QA").Sum(c => c.Idade);
        Console.WriteLine($"Soma das idades dos candidatos de QA: {somaIdadesQA} anos\n");
    }

    static void ExibirEstadosDistintos(List<Candidato> candidatos)
    {
        var estadosDistintos = candidatos.Select(c => c.Estado).Distinct().Count();
        Console.WriteLine($"Número de estados distintos presentes entre os candidatos: {estadosDistintos}\n");
    }

    static void CriarArquivoCSV(List<Candidato> candidatos)
    {
        var candidatosOrdenados = candidatos.OrderBy(c => c.Nome).ToList(); 

        using (StreamWriter sw = new StreamWriter("Sorted_Academy_Candidates.csv"))
        {
            sw.WriteLine("Nome,Idade,Estado,Vaga");
            foreach (var candidato in candidatosOrdenados)
            {
                sw.WriteLine($"{candidato.Nome},{candidato.Idade},{candidato.Estado},{candidato.Vaga}");
            }
        }

        Console.WriteLine("Arquivo Sorted_Academy_Candidates.csv criado com sucesso.\n");
    }

    static void DescobrirInstrutorQA(List<Candidato> candidatos)
    {
        var instrutorQA = candidatos.FirstOrDefault(c =>
            c.Vaga == "QA" &&
            c.Estado == "SC" &&
            Math.Sqrt(c.Idade) % 1 == 0 && 
            c.Idade >= 18 && c.Idade <= 30 &&
            c.Nome.SequenceEqual(c.Nome.Reverse()) 
        );

        if (instrutorQA != null)
        {
            Console.WriteLine($"O nome do instrutor de QA é: {instrutorQA.Nome}\n");
        }
        else
        {
            Console.WriteLine("Instrutor de QA não encontrado.\n");
        }
    }

    static void DescobrirInstrutorMobile(List<Candidato> candidatos)
    {
        var instrutorMobile = candidatos.FirstOrDefault(c =>
            c.Vaga == "Mobile" &&
            c.Estado == "PI" &&
            c.Idade % 2 == 0 &&
            c.Idade >= 30 && c.Idade <= 40 &&
            c.Nome.StartsWith("C") 
        );

        if (instrutorMobile != null)
        {
            Console.WriteLine($"O nome do instrutor de Mobile é: {instrutorMobile.Nome}\n");
        }
        else
        {
            Console.WriteLine("Instrutor de Mobile não encontrado.\n");
        }
    }
}

using System;
using dio.series.Classes;


namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        Console.Clear();
                        ListarSeries();
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        InserirSerie();
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        AtualizarSerie();
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();
                        ExcluirSerie();
                        Console.ReadLine();
                        break;
                    case "5":
                        Console.Clear();
                        VisualizarSerie();
                        Console.ReadLine();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
            Console.Clear();
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Visualizar Série");
            Console.WriteLine("================");
            Console.WriteLine("");

            Console.Write("Digite o ID da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.retornaPorId(indiceSerie);

            Console.WriteLine(serie);
            Console.ReadLine();
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Excluir Série");
            Console.WriteLine("===============");
            Console.WriteLine("");

            Console.Write("Digite o ID da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            bool respostaInvalida = true;
            string resposta = "";

            //Ficará perguntando
            //enquanto a reposta for diferente de 'S' ou 'N'
            while (respostaInvalida)
            {
                Console.Write("Deseja realmente Excluir, (S/N)?: ");
                resposta = Console.ReadLine();
                if (resposta.ToUpper() != "S" && resposta.ToUpper() != "N")
                {
                    Console.Write("Reposta inválida! ");
                    respostaInvalida = true;
                }
                else
                {
                    if (resposta.ToUpper() == "S")
                        repositorio.Exclui(indiceSerie);

                    respostaInvalida = false;
                }
            }
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Atualizar Série");
            Console.WriteLine("===============");
            Console.WriteLine("");
            Console.Write("Digite o ID da Série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine("Digite o Genero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: repositorio.ProximoId(),
                        genero: (Genero)entradaGenero,
                        titulo: entradaTitulo,
                        ano: entradaAno,
                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir Nova Série");
            Console.WriteLine("==================");
            Console.WriteLine("");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o Genero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de Início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                        genero: (Genero)entradaGenero,
                        titulo: entradaTitulo,
                        ano: entradaAno,
                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
            Console.WriteLine("");
            Console.WriteLine("* Registro Inserido com sucesso! *");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar Séries");
            Console.WriteLine("=============");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma Série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "SIM" : "NÃO"));
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe as opção desejada");

            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Série");
            Console.WriteLine("3 - Atualizar Série");
            Console.WriteLine("4 - Excluir Série");
            Console.WriteLine("5 - Visualizar Série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;

        }
    }
}

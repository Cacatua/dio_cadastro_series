// See https://aka.ms/new-console-template for more information

using DIO.Series.Classes;
using DIO.Series.Enum;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            var cOpcaoUsuario = MenuSistema();

            while (ExecutaOpcaoUsuario(cOpcaoUsuario))
            {
                 cOpcaoUsuario = MenuSistema();
            }
        }

        private static bool ExecutaOpcaoUsuario(string cOpcaoUsuario)
        {
            bool continuarExecutando = true;

            Console.Clear();

            switch (cOpcaoUsuario)
            {
                case OpcoesUsuario.listarSeries:
                    ListarSeries();
                    break;
                case OpcoesUsuario.inserirSerie:
                    InserirSerie();
                    break;
                case OpcoesUsuario.atualizarSerie:
                    AtualizarSerie();
                    break;
                case OpcoesUsuario.excluirSerie:
                    ExcluirSerie();
                    break;
                case OpcoesUsuario.visualizarSerie:
                    VisualizarSerie();
                    break;
                // case OpcoesUsuario.limparTela:
                //     Console.Clear();
                //     break;
                case OpcoesUsuario.sair:
                    continuarExecutando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (continuarExecutando)
            {
                Console.WriteLine("Pressione [Enter] para continuar.");
                Console.ReadKey();
                Console.Clear();
            }

            return continuarExecutando;
        }

        private static void VisualizarSerie()
        {
            System.Console.WriteLine("Visualizar série");
            System.Console.Write("Digite o id da série: ");
            int Id = int.Parse(Console.ReadLine());

            if (!repositorio.Existe(Id))
            {
                System.Console.WriteLine("ID não encontrado.");
                return;
            }

            var serie = repositorio.RetornaPorId(Id);

            System.Console.WriteLine(serie);
        }

        private static void ExcluirSerie()
        {
            System.Console.WriteLine("Excluir série");
            System.Console.Write("Digite o id da série: ");
            int Id = int.Parse(Console.ReadLine());

            if (!repositorio.Existe(Id))
            {
                System.Console.WriteLine("ID não encontrado.");
                return;
            }

            repositorio.Exclui(Id);
            System.Console.WriteLine("Série excluida");
        }

        private static void AtualizarSerie()
        {
            System.Console.WriteLine("Atualizar série");

            System.Console.Write("Digite o id da série: ");
            int Id = int.Parse(Console.ReadLine());

            if (!repositorio.Existe(Id))
            {
                System.Console.WriteLine("ID não encontrado.");
                return;
            }

            Serie novaSerie = ObtemInformacaoSerie(Id);

            repositorio.Atualiza(Id, novaSerie);
            System.Console.WriteLine("Série atualizada");
        }

        private static void InserirSerie()
        {
            System.Console.WriteLine("Inserir nova série");

            Serie novaSerie = ObtemInformacaoSerie(repositorio.ProximoId());

            repositorio.Insere(novaSerie);
            System.Console.WriteLine("Série inserida");
        }

        private static Serie ObtemInformacaoSerie(int Id)
        {
            foreach (int i in Enum.Genero.GetValues(typeof(Genero)))
            {
                System.Console.WriteLine($"{i}-{Enum.Genero.GetName(typeof(Genero), i)}");
            }
            Console.Write("Digite o genêro entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(Id, (Genero)entradaGenero, entradaTitulo, entradaDescricao, entradaAno);

            return novaSerie;
        }

        private static void ListarSeries()
        {
            System.Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhuma série cadastrada.");
            }
            else
            {
                foreach (var serie in lista)
                {
                    Console.WriteLine($"#ID {serie.retornaId()}: {serie.retornaTitulo()}");
                }
            }
        }

        private static string MenuSistema()
        {
            Console.WriteLine("DIO Séries a seu dispor!");
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine($"{OpcoesUsuario.listarSeries} - Listar séries");
            Console.WriteLine($"{OpcoesUsuario.inserirSerie} - Inserir nova série");
            Console.WriteLine($"{OpcoesUsuario.atualizarSerie} - Atualizar série");
            Console.WriteLine($"{OpcoesUsuario.excluirSerie} - Excluir série");
            Console.WriteLine($"{OpcoesUsuario.visualizarSerie} - Visualizar série");
            // Console.WriteLine($"{OpcoesUsuario.limparTela} - Limpar tela");
            Console.WriteLine($"{OpcoesUsuario.sair} - Sair");

            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }
    }
}
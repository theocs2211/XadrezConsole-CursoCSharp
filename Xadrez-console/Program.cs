using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirPartida(partida, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);

                        if (partida.PecaPromovida != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Peão promovido! Escolha a peça para promoção:");
                            Console.WriteLine("[D] Dama  [T] Torre  [B] Bispo  [C] Cavalo");
                            Console.Write("Opção: ");
                            char T = char.Parse(Console.ReadLine());
                            partida.PromoverPeca(T);
                        }
                    }
                    catch(TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();

                    }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("Posição invalida!");
                        Console.ReadKey();
                    } 
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message); 
            }
        }
    }
}
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;

namespace XadrezConsole
{
    internal class Tela
    {
         public static void ImprimirTabuleiro(Tabuleiro tab)
         {
             for (int i = 0; i < tab.Linhas; i++)
             {
                 Console.Write(8 - i + " ");
                 for (int j = 0; j < tab.Colunas; j++)
                 {
                     ImprimirPeca(tab.Peca(i, j));
                 }
                 Console.WriteLine();
             }
             Console.WriteLine("  a b c d e f g h");
         }
         public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
         {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

             for (int i = 0; i < tab.Linhas; i++)
             {
                 Console.Write(8 - i + " ");
                 for (int j = 0; j < tab.Colunas; j++)
                 {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }                                                                
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                 Console.WriteLine();
             }
             Console.WriteLine("  a b c d e f g h");
         }

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                if (partida.Empate)
                {
                    Console.WriteLine("EMPATE!");
                }
                else if (partida.XequeMate)
                {
                    Console.WriteLine("XEQUEMATE!");
                    Console.WriteLine("Vencedor: " + partida.JogadorAtual);
                }
            }
        }
        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] posicoesPossiveis)
        {
            ImprimirTabuleiro(partida.Tab, posicoesPossiveis);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                if (partida.Empate) 
                {
                    Console.WriteLine("EMPATE!");
                }
                else if (partida.XequeMate)
                {
                    Console.WriteLine("XEQUEMATE!");
                    Console.WriteLine("Vencedor: " + partida.JogadorAtual);
                }
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas:");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas:");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca p in conjunto)
            {
                if (p.Cor == Cor.Preta) {Console.ForegroundColor = ConsoleColor.DarkYellow;}
                Console.Write(p + " "); 
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("] ");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                    Console.Write(" ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(peca);
                    Console.Write(" ");
                    Console.ForegroundColor = aux;
                }
            }
        }
    }
}

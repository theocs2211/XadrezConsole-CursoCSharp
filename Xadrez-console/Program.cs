using System;
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;
using Xadrez_console.tabuleiro;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez posX = new PosicaoXadrez('a', 1);

            Console.WriteLine(posX);

            Console.WriteLine(posX.ToPosicao());
        }
    }
}
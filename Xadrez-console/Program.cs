using System;
using XadrezConsole.tabuleiro;
using XadrezConsole.pecas;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.AdicionarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
            tab.AdicionarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
            tab.AdicionarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
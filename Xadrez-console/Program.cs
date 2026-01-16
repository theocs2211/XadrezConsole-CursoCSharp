using System;
using XadrezConsole.tabuleiro;
using XadrezConsole.xadrez;
using Xadrez_console.tabuleiro;
using System.Net.Http.Headers;

namespace XadrezConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.AdicionarPeca(new Torre(Cor.Preta, tab), new Posicao(0,0));
            tab.AdicionarPeca(new Rei(Cor.Branca, tab), new Posicao(1,0));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
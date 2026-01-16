using System;

namespace Xadrez_console.tabuleiro
{
    internal class TabuleiroException : Exception
    {
        public TabuleiroException(string message) : base(message)
        {
        }
    }
}

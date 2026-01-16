namespace XadrezConsole.Tabuleiro
{
    internal class Peca
    {
        public Posicao Pos { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Posicao pos, Cor cor, Tabuleiro tab)
        {
            Pos = pos;
            Cor = cor;
            Tab = tab;
            QteMovimentos = 0;
        }
    }
}

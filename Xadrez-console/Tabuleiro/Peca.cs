namespace XadrezConsole.tabuleiro
{
    internal class Peca
    {
        public Posicao Pos { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Pos = null;
            Cor = cor;
            Tab = tab;
            QteMovimentos = 0;
        }
    }
}

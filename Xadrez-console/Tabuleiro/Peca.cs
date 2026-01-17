namespace XadrezConsole.tabuleiro
{
    abstract class Peca
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

        public abstract bool[,] MovimentosPosiveis();

        public void IncrementarQteMovimentos()
        {
            QteMovimentos++;
        }

        protected bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != Cor;
        }
    }
}

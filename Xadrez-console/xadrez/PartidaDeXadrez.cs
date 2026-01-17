using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogardorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogardorAtual = Cor.Branca;
            Terminada = false;
            AdicionarPecas();
        }

        private void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.AdicionarPeca(p, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogardorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void MudaJogador()
        {
            if (JogardorAtual == Cor.Branca)
            {
                JogardorAtual = Cor.Preta;
            }
            else
            {
                JogardorAtual = Cor.Branca;
            }
        }

        private void AdicionarPecas()
        {
            Tab.AdicionarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.AdicionarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.AdicionarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.AdicionarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.AdicionarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}

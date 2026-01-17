using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogardorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogardorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            AdicionarPecas();
        }

        private void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.AdicionarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Capturadas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p); 
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void AdicionarPecas()
        {
            AdicionarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

            AdicionarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));
        }
    }
}

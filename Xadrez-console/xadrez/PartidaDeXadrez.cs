using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public bool XequeMate { get; private set; }
        public bool Empate { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool xeque;
        public Peca VulneravelElPassant { get; private set; }
        public Peca PecaPromovida { get; set; }


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            XequeMate = false;
            xeque = false;
            VulneravelElPassant = null;
            PecaPromovida = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            AdicionarPecas();
        }

        private Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RemoverPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.AdicionarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //Jogada especial: Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RemoverPeca(origemT);
                T.IncrementarQteMovimentos();
                Tab.AdicionarPeca(T, destinoT);
            }
            //Jogada especial: Roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RemoverPeca(origemT);
                T.IncrementarQteMovimentos();
                Tab.AdicionarPeca(T, destinoT);
            }

            //Jogada especial: En passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = Tab.RemoverPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RemoverPeca(destino);
            p.DecrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                Tab.AdicionarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.AdicionarPeca(p, origem);


            //Jogada especial: Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RemoverPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tab.AdicionarPeca(T, origemT);
            }

            //Jogada especial: Roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RemoverPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tab.AdicionarPeca(T, origemT);
            }

            //Jogada especial: En passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelElPassant)
                {
                    Peca peao = Tab.RemoverPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.AdicionarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tab.Peca(destino);
            //Jogada especial: Promoção
            if (p is Peao)
            {
                if (p.Cor == Cor.Branca && destino.Linha == 0 || p.Cor == Cor.Preta && destino.Linha == 7)
                {
                    PecaPromovida = p;
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual) ))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (EstaEmpatado())
            {
                Terminada = true;
                Empate = true;
            }
            else if (EstaEmXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
                XequeMate = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            //Jogada especial: En passant
            if (p is Peao && (origem.Linha == destino.Linha - 2 || origem.Linha == destino.Linha + 2))
            {
                VulneravelElPassant = p;
            }
            else
            {
                VulneravelElPassant = null;
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor)
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
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
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

        private static Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca) { return Cor.Preta; }
            else { return Cor.Branca; }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca p in PecasEmJogo(Adversaria(cor)))
            {
                    bool[,] mat = p.MovimentosPossiveis();
                if (mat[R.Pos.Linha, R.Pos.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EstaEmXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor)) 
            { 
                return false; 
            }
            foreach (Peca p in PecasEmJogo(cor))
            {
                bool[,] mat = p.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = p.Pos;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public bool EstaEmpatado()
        {
            //Material insuficiente
            HashSet<Peca> pecasEmJogo = PecasEmJogo(Cor.Branca);
            pecasEmJogo.UnionWith(PecasEmJogo(Cor.Preta));
            //Rei vs Rei
            if (pecasEmJogo.Count == 2)
            {
                return true;
            }
            //Rei vs Rei + Cavalo ou Rei vs Rei + Bispo
            else if (pecasEmJogo.Count == 3)
            {
                int aux = 0;
                foreach (Peca p in pecasEmJogo)
                {
                    if (p is Rei)
                    {
                        aux++;
                    }
                    if (p is Cavalo || p is Bispo)
                    {
                        aux++;
                    }
                }
                return aux == 3;
            }
            //Rei + Bispo vs Rei + Bispo    Os dois bispos tem que ser da mesma cor
            else if (pecasEmJogo.Count == 4 && PecasEmJogo(Cor.Branca).Count == 2)
            {
                int aux = 0;
                int Cor1 = 0;
                int Cor2 = 0;
                List<Peca> bispos = new List<Peca>();
                foreach (Peca p in PecasEmJogo(Cor.Branca))
                {
                    if (p is Rei)
                    {
                        aux++;
                    }
                    if (p is Bispo)
                    {
                        aux++;
                        bispos.Add(p);
                    }
                }
                if (aux == 2)
                {
                    foreach (Peca p in PecasEmJogo(Cor.Preta))
                    {
                        if (p is Rei)
                        {
                            aux++;
                        }
                        if (p is Bispo) 
                        {
                            aux++;
                            bispos.Add(p);
                        }
                    }
                }
                if (bispos.Count == 2)
                {
                    foreach (Peca b in bispos)
                    {
                        if ((b.Pos.Linha % 2 == 0 && b.Pos.Coluna % 2 == 0) || (b.Pos.Linha % 2 == 1 && b.Pos.Coluna % 2 == 1)) 
                        {
                            Cor1++;
                        }
                        else
                        {
                            Cor2++;
                        }
                    }
                }
                return aux == 4 && (Cor1 == 2) || (Cor2 == 2);
            }
            //Rei vs Rei + 2 Cavalos
            else if (pecasEmJogo.Count == 4)
            {
                int aux = 0;
                foreach (Peca p in PecasEmJogo(Cor.Branca))
                {
                    if (p is Rei)
                    {
                        aux++;
                    }
                    if (p is Cavalo)
                    {
                        aux++;
                    }
                }
                if (aux == 1 || aux == 3)
                {
                    foreach (Peca p in PecasEmJogo(Cor.Preta))
                    {
                        if (p is Rei)
                        {
                            aux++;
                        }
                        if (p is Cavalo)
                        {
                            aux++;
                        }
                    }
                }
                return aux == 4;
            }
            else { return false; }
        }

        public void PromoverPeca(char promoverPecaPara)
        {
            Peca p = PecaPromovida;
            Posicao aux = p.Pos;
            Peca peca;

            Tab.RemoverPeca(aux);
            Pecas.Remove(p);
            if (promoverPecaPara == 'D' || promoverPecaPara == 'd')
            {
                peca = new Dama(p.Cor, Tab);
            }
            else if (promoverPecaPara == 'T' || promoverPecaPara == 't')
            {
                peca = new Torre(p.Cor, Tab);
            }
            else if (promoverPecaPara == 'B' || promoverPecaPara == 'b')
            {
                peca = new Bispo(p.Cor, Tab);
            }
            else if (promoverPecaPara == 'C' || promoverPecaPara == 'c')
            {
                peca = new Cavalo(p.Cor, Tab);
            }
            else
            {
                throw new TabuleiroException("Peça invalida!");
            }
            Tab.AdicionarPeca(peca, aux);
            Pecas.Add(peca);
            PecaPromovida = null;
        }

        public void AdicionarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AdicionarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void AdicionarPecas()
        { /*
            AdicionarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            AdicionarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            AdicionarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            AdicionarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            AdicionarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            AdicionarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            AdicionarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            AdicionarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
            AdicionarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

            AdicionarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            AdicionarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            AdicionarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            AdicionarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            AdicionarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            AdicionarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            AdicionarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            AdicionarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('h', 7, new Peao(Cor.Preta, Tab, this));
            */
            AdicionarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            AdicionarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            AdicionarNovaPeca('d', 2, new Peao(Cor.Preta, Tab, this));
            AdicionarNovaPeca('a', 8, new Cavalo(Cor.Preta, Tab));
            AdicionarNovaPeca('a', 7, new Cavalo(Cor.Preta, Tab));
        }
    }
}
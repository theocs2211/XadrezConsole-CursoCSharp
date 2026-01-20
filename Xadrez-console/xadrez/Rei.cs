using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class Rei : Peca
    {
        private PartidaDeXadrez Partida;

        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }
        
        public override string ToString()
        {
            return "R";
        }

        private bool TesteTorreParaRoque(Posicao pos) 
        {
            Peca p = Tab.Peca(pos);
            return p != null && p.QteMovimentos == 0 && p is Torre && p.Cor == this.Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas,Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Nordeste
            pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Direita
            pos.DefinirPosicao(Pos.Linha, Pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sudeste
            pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Abaixo
            pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sudoeste
            pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Esquerda
            pos.DefinirPosicao(Pos.Linha, Pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Noroeste
            pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Jogada especial: Roque
            if (QteMovimentos == 0 && !Partida.xeque)
            {
                //Jogada especial: Roque pequeno
                Posicao PosT1 = new Posicao(Pos.Linha, Pos.Coluna + 3);
                if (TesteTorreParaRoque(PosT1))
                {
                    Posicao p1 = new Posicao(Pos.Linha, Pos.Coluna + 1);
                    Posicao p2 = new Posicao(Pos.Linha, Pos.Coluna + 2);
                    if (!Tab.ExistePeca(p1) && !Tab.ExistePeca(p2))
                    {
                        mat[Pos.Linha, Pos.Coluna + 2] = true;
                    }
                }
                //Jogada especial: Roque grande
                Posicao PosT2 = new Posicao(Pos.Linha, Pos.Coluna - 4);
                if (TesteTorreParaRoque(PosT1))
                {
                    Posicao p1 = new Posicao(Pos.Linha, Pos.Coluna - 1);
                    Posicao p2 = new Posicao(Pos.Linha, Pos.Coluna - 2);
                    Posicao p3 = new Posicao(Pos.Linha, Pos.Coluna - 3);
                    if (!Tab.ExistePeca(p1) && !Tab.ExistePeca(p2) && !Tab.ExistePeca(p3))
                    {
                        mat[Pos.Linha, Pos.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}

using System.Runtime.Intrinsics.X86;
using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez Partida;

        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool TemInimigo(Posicao pos)
        {
            return Tab.Peca(pos) != null && Tab.Peca(pos).Cor != Cor;
        }

        private bool EstaLivre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                //1 para frente
                pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna);
                if (Tab.PosicaoValida(pos) && EstaLivre(pos)) 
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //2 para frente
                pos.DefinirPosicao(Pos.Linha - 2, Pos.Coluna);
                Posicao aux = new Posicao(pos.Linha + 1, pos.Coluna);
                if (Tab.PosicaoValida(pos) && EstaLivre(pos) && EstaLivre(aux) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //Noroeste
                pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna - 1);
                if (Tab.PosicaoValida(pos) && TemInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //Nordeste
                pos.DefinirPosicao(Pos.Linha - 1, Pos.Coluna + 1);
                if (Tab.PosicaoValida(pos) && TemInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial: En passant
                if (Pos.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Pos.Linha, Pos.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && TemInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelElPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Pos.Linha, Pos.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && TemInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelElPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                //1 para frente
                pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna);
                if (Tab.PosicaoValida(pos) && EstaLivre(pos)) 
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //2 para frente
                pos.DefinirPosicao(Pos.Linha + 2, Pos.Coluna);
                Posicao aux = new Posicao(pos.Linha - 1, pos.Coluna);
                if (Tab.PosicaoValida(pos) && EstaLivre(pos) && EstaLivre(aux) && QteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //Sudoeste
                pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna - 1);
                if (Tab.PosicaoValida(pos) && TemInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                //Nordeste
                pos.DefinirPosicao(Pos.Linha + 1, Pos.Coluna + 1);
                if (Tab.PosicaoValida(pos) && TemInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                //Jogada especial: En passant
                if (Pos.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Pos.Linha, Pos.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && TemInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VulneravelElPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(Pos.Linha, Pos.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && TemInimigo(direita) && Tab.Peca(direita) == Partida.VulneravelElPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}

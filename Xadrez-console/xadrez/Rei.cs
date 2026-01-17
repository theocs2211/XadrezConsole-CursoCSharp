using XadrezConsole.tabuleiro;

namespace XadrezConsole.xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }
        
        public override string ToString()
        {
            return "R";
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

            return mat;
        }
    }
}

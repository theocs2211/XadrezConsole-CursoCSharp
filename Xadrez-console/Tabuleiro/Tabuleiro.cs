using Xadrez_console.tabuleiro;

namespace XadrezConsole.tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }
        public Peca Peca(Posicao pos)
        {
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        public void AdicionarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            else
            {
                Pecas[pos.Linha, pos.Coluna] = p;
                p.Pos = pos;
            }
        }

        public Peca RemoverPeca(Posicao pos)
        {
            if (ExistePeca(pos))
            {
                Peca p = Pecas[pos.Linha, pos.Coluna];
                Pecas[pos.Linha, pos.Coluna] = null;
                p.Pos = null;
                return p;
            }
            else
            {
                //throw new TabuleiroException("Não existe nenhuma peça nesta posição!");
                return null;
            }
        }

        private bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}

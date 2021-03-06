using ConsoleApp1.TabuleiroEx;
using Xadrez;

namespace Xadrez
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        public Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca PecaChamar(int linha, int coluna)
        {
            Posicao pos = new Posicao(linha, coluna);
            ValidarPosicao(pos);
            return Pecas[linha, coluna];
        }

        public Peca PecaChamar(Posicao pos)
        {
            ValidarPosicao(pos);
            return Pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return PecaChamar(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)

		{
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Ja existe uma peça nessa posição!");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.PosicaoPeca = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if(PecaChamar(pos) == null)
            {
                return null;
            }
            Peca aux = PecaChamar(pos);
            aux.PosicaoPeca = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
			{
                throw new TabuleiroException("Posicao Inválida");
            }
        }
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace ConsoleApp1.Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.PecaChamar(pos);
			if (p == null || p.CorPeca != this.CorPeca)
				return true;
			return false;
		}

		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			pos.DefinirValores(PosicaoPeca.Linha -2, PosicaoPeca.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha -2, PosicaoPeca.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}




			return mat;
		}

		public override string ToString()
        {
            return " C ";
        }
    }
}

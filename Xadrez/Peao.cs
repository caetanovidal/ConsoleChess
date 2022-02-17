using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace ConsoleApp1.Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

		//private bool PodeMover(Posicao pos)
		//{
		//	Peca p = Tab.PecaChamar(pos);
		//	if (p == null || p.CorPeca != this.CorPeca)
		//		return true;
		//	return false;
		//}

		private bool _existePeca(Posicao pos)
		{
			Peca p = Tab.PecaChamar(pos);
			return p != null;
		}

		private bool _existeInimigo(Posicao pos)
		{
			Peca p = Tab.PecaChamar(pos);
			return p != null && p.CorPeca != CorPeca;

		}



		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);
			//acima
			
			if (CorPeca == Cor.Branca)
			{
				pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
				if (Tab.PosicaoValida(pos) && !_existePeca(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna);
				if (Tab.PosicaoValida(pos) && !_existePeca(pos) && QuantidadeMovimentos == 0)
				{
					mat[pos.Linha, pos.Coluna] = true;
				}

				pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
				if (Tab.PosicaoValida(pos) && _existeInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}

				pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
				if (pos.Coluna <= 7)
				{
					if (Tab.PosicaoValida(pos) && _existeInimigo(pos))
					{
						mat[pos.Linha, pos.Coluna] = true;
					}
				}
				
			}
			else
			{
				pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
				if (Tab.PosicaoValida(pos) && !_existePeca(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
				pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna);
				if (Tab.PosicaoValida(pos) && !_existePeca(pos) && QuantidadeMovimentos == 0)
				{
					mat[pos.Linha, pos.Coluna] = true;
				}


				pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
				if (Tab.PosicaoValida(pos) && _existeInimigo(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}

				pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
				if (pos.Coluna <= 7)
				{
					if (Tab.PosicaoValida(pos) && _existeInimigo(pos))
					{
						mat[pos.Linha, pos.Coluna] = true;
					}
				}
				
			}

				

			return mat;
		}

		public override string ToString()
        {
            return " P ";
        }
    }
}

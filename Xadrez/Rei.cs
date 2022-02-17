using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace ConsoleApp1.Xadrez
{
    class Rei : Peca
    {
		private PartidaDeXadrez _partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
			_partida = partida;
        }

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.PecaChamar(pos);
			if (p == null || p.CorPeca != this.CorPeca)
				return true;
			return false;
		}

		private bool _testeTorreParaRoque(Posicao pos)
		{
			Peca p = Tab.PecaChamar(pos);
			return p != null && p is Torre && p.CorPeca == CorPeca && p.QuantidadeMovimentos == 0;
		}

		public override bool[,] MovimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			//acima
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// ne
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
			if(pos.Coluna <= 7)
			{
				if (Tab.PosicaoValida(pos) && PodeMover(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
			}
			
			// direita
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
			if (pos.Coluna <= 7)
			{
				if (Tab.PosicaoValida(pos) && PodeMover(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
			}
			
			// se
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);

			if (pos.Coluna <= 7)
			{
				if (Tab.PosicaoValida(pos) && PodeMover(pos))
				{
					mat[pos.Linha, pos.Coluna] = true;
				}
			}
			
			// abaixo
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// so
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// esquerda
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// no
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}


			// jogada especial roque

			if (QuantidadeMovimentos==0 && !_partida.Xeque)
			{
				// jogada especial roque pequeno
				Posicao posT1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 3);
				if (_testeTorreParaRoque(posT1))
				{
					Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
					Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 2);

					if (Tab.PecaChamar(p1) == null && Tab.PecaChamar(p2) == null)
					{
						mat[PosicaoPeca.Linha, PosicaoPeca.Coluna + 2] = true;
					}
				}

				// jogada especial roque grande
				Posicao posT2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 4);
				if (_testeTorreParaRoque(posT2))
				{
					Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
					Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 2);
					Posicao p3 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 3);

					if (Tab.PecaChamar(p1) == null && Tab.PecaChamar(p2) == null && Tab.PecaChamar(p3) == null)
					{
						mat[PosicaoPeca.Linha, PosicaoPeca.Coluna - 2] = true;
					}
				}


			}


			return mat;
		}

		public override string ToString()
        {
            return " R ";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace ConsoleApp1.Xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
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

			Posicao pos = new Posicao(0, 3);

			//acima
			//pos.DefinirValores(pos.Linha - 1, pos.Coluna);
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// ne
			//pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// direita
			//pos.DefinirValores(pos.Linha, pos.Coluna + 1);
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// se
			//pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// abaixo
			//pos.DefinirValores(pos.Linha + 1, pos.Coluna);
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// so
			//pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// esquerda
			//pos.DefinirValores(pos.Linha, pos.Coluna - 1);
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);

			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}

			// no
			//pos.DefinirValores(pos.Linha -1, pos.Coluna - 1);
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
			}


			return mat;
		}

		public override string ToString()
        {
            return " R ";
        }
    }
}

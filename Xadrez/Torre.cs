using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez;

namespace ConsoleApp1.Xadrez
{
    class Torre : Peca 
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
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


			//acima
			pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.ExistePeca(pos) && Tab.PecaChamar(pos).CorPeca != this.CorPeca)
				{
					break;
				}
				pos.Linha -= 1;
			}

			//direita
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.ExistePeca(pos) && Tab.PecaChamar(pos).CorPeca != this.CorPeca)
				{
					break;
				}
				pos.Coluna += 1;
			}

			//abaixo
			pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.ExistePeca(pos) && Tab.PecaChamar(pos).CorPeca != this.CorPeca)
				{
					break;
				}
				pos.Linha += 1;
			}

			//esquerda
			pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.ExistePeca(pos) && Tab.PecaChamar(pos).CorPeca != this.CorPeca)
				{
					break;
				}
				pos.Coluna -= 1;
			}



			return mat;
		}

		public override string ToString()
        {
            return " T ";
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez
{
    public abstract class Peca
    {
        public Posicao PosicaoPeca { get; set; }
        public Cor CorPeca { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            PosicaoPeca = null;
            Tab = tab;
            CorPeca = cor;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarQuantMovimentos()
        {
            QuantidadeMovimentos++;
        }

		public bool ExisteMovimentosPossiveis()
		{
			bool[,] mat = MovimentosPossiveis();
			for(int i=0; i<Tab.Linhas; i++)
			{
				for (int j=0; j<Tab.Colunas; j++)
				{
					if (mat[i, j])
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool PodeMoverPara(Posicao pos)
		{
			return MovimentosPossiveis()[pos.Linha, pos.Coluna];
		}

		public abstract bool[,] MovimentosPossiveis();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Xadrez;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i=0; i<tab.Colunas; i++)
            {
                Console.Write($" {8 - i} ");
                for(int j=0; j<tab.Linhas; j++)
                {
					ImprimirPeca(tab.Pecas[i, j]);
				}
				Console.WriteLine();
            }
            Console.WriteLine("    a  b  c  d  e  f  g  h ");
        }

		public static void ImprimirPartida(PartidaDeXadrez partida)
		{
			ImprimirTabuleiro(partida._tab);
			Console.WriteLine();
			ImprimirPecasCapturadas(partida);
			Console.WriteLine($"Turno {partida._turno}");
			Console.WriteLine("Jogador atual: " + partida._jogadorAtual);
			Console.WriteLine();
		}

		private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
		{
			Console.WriteLine("Peças capturadas: ");
			Console.Write("Brancas: ");
			ImprimirConjunto(partida.pecasCapturadas(Cor.Branca));

			Console.Write("Pretas: ");
			ConsoleColor aux = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
			Console.WriteLine();

			Console.ForegroundColor = aux;

		}

		public static void ImprimirConjunto(HashSet<Peca> conjunto)
		{
			Console.Write("[");
			foreach (Peca p in conjunto)
			{
				Console.Write(p + " ");
			}
			Console.Write("]");
			Console.WriteLine();
		}

		public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] PosicoesPossiveis)
		{
			ConsoleColor fundoOriginal = Console.BackgroundColor;
			ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

			for (int i = 0; i < tab.Colunas; i++)
			{
				Console.Write($" {8 - i} ");
				for (int j = 0; j < tab.Linhas; j++)
				{
					if (PosicoesPossiveis[i, j])
					{
						Console.BackgroundColor = fundoAlterado;
					}
					else
					{
						Console.BackgroundColor = fundoOriginal;
					}
					ImprimirPeca(tab.Pecas[i, j]);
					Console.BackgroundColor = fundoOriginal;
				}
				Console.WriteLine();
			}
			Console.WriteLine("    a  b  c  d  e  f  g  h ");
			Console.BackgroundColor = fundoOriginal;
		}

		public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
			if (peca == null)
			{
				Console.Write(" - ");
			}
			else
			{
				if (peca.CorPeca == Cor.Branca)
				{
					Console.Write(peca);
				}
				else
				{
					ConsoleColor aux = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write(peca);
					Console.ForegroundColor = aux;
				}
			}

			
        }
    }
}

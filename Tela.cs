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
                    if (i == 1)
                    {
                        Posicao pos = new Posicao(i, j);
                        tab.ColocarPeca(new Peao(tab, Cor.Branca), pos);
                    }
                    if (i == 6)
                    {
                        Posicao pos = new Posicao(i, j);
                        tab.ColocarPeca(new Peao(tab, Cor.Vermelha), pos);
                    }
                    if (  tab.PecaChamar(i,j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        ImprimirPeca(tab.Pecas[i,j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("    a  b  c  d  e  f  g  h ");
        }

        public static void ImprimirPeca(Peca peca)
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

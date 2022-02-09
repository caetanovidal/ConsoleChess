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
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i=0; i<tab.Colunas; i++)
            {
                for(int j=0; j<tab.Linhas; j++)
                {
                    if (i == 1)
                    {
                        Posicao pos = new Posicao(i, j);
                        tab.colocarPeca(new Peao(tab, Cor.Branca), pos);
                    }
                    if (i == 6)
                    {
                        Posicao pos = new Posicao(i, j);
                        tab.colocarPeca(new Peao(tab, Cor.Vermelha), pos);
                    }
                    if (  tab.pecaChamar(i,j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write(tab.Pecas[i,j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

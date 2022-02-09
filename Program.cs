using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);



            tab.colocarPeca(new Rei(tab, Cor.Vermelha), new Posicao(0, 0));
            tab.colocarPeca(new Rei(tab, Cor.Branca), new Posicao(2, 4));
            tab.colocarPeca(new Torre(tab, Cor.Vermelha), new Posicao(1, 3));
            tab.colocarPeca(new Torre(tab, Cor.Branca), new Posicao(0, 6));


            Tela.imprimirTabuleiro(tab);

        }
    }
}

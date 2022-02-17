using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Xadrez;
using ConsoleApp1.TabuleiroEx;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();


                while (!partida.Terminada)
                {
					try
					{
						Console.Clear();
						Tela.ImprimirPartida(partida);

						Console.Write("Origem: ");
						Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
						partida.ValidarPosicaoDeOrigem(origem);

						bool[,] posicoesPossivels = partida._tab.PecaChamar(origem).MovimentosPossiveis();

						Console.Clear();
						Tela.ImprimirTabuleiro(partida._tab, posicoesPossivels);

						Console.Write("Destino: ");
						Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
						partida.ValidarPosicaoDeDestino(origem, destino);

						partida.RealizaJogada(origem, destino);
					}
                    catch (TabuleiroException e)
					{
						Console.WriteLine(e.Message);
						Console.ReadLine();
					}
                }


            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}

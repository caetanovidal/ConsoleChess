using ConsoleApp1.TabuleiroEx;
using ConsoleApp1.Xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro _tab { get; private set; }
        public int _turno { get; private set; }
        public Cor _jogadorAtual { get; private set; } 
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            _tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            ColocarPecas();
            Terminada = false;
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = _tab.RetirarPeca(origem);
            p.IncrementarQuantMovimentos();
            Peca pecaCapturada = _tab.RetirarPeca(destino);
            _tab.ColocarPeca(p, destino);

        }

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			ExecutaMovimento(origem, destino);
			_turno++;
			MudaJogador();
		}

		public void ValidarPosicaoDeOrigem(Posicao pos)
		{
			if (_tab.PecaChamar(pos) == null)
			{
				throw new TabuleiroException("Não existe peça na posicão de origem escolhida!");
			}
			if (_jogadorAtual != _tab.PecaChamar(pos).CorPeca)
			{
				throw new TabuleiroException("A peça de origem escolhida não é sua!");
			}
			if (!_tab.PecaChamar(pos).ExisteMovimentosPossiveis())
			{
				throw new TabuleiroException("Peça sem movimentos possiveis!");
			}
		}

		public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
		{
			 if (!_tab.PecaChamar(origem).PodeMoverPara(destino))
			{
				throw new TabuleiroException("Posição de destino invalida!");
			}
		}

		private void MudaJogador()
		{
			if (_jogadorAtual == Cor.Branca)
			{
				_jogadorAtual = Cor.Preta;
			}
			else
			{
				_jogadorAtual = Cor.Branca;
			}
		}

        private void ColocarPecas()
        {
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('d', 7).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('c', 7).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('e', 7).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('c', 8).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('e', 8).ToPosicao());

			_tab.ColocarPeca(new Rei(_tab, Cor.Branca), new PosicaoXadrez('d', 8).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('a', 8).ToPosicao());


			_tab.ColocarPeca(new Torre(_tab, Cor.Preta), new PosicaoXadrez('a', 1).ToPosicao());
            _tab.ColocarPeca(new Torre(_tab, Cor.Preta), new PosicaoXadrez('h', 1).ToPosicao());

        }

    }
}

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
		private HashSet<Peca> _pecas;
		private HashSet<Peca> _capturadas;

        public PartidaDeXadrez()
        {
            _tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
			_pecas = new HashSet<Peca>();
			_capturadas = new HashSet<Peca>();
            ColocarPecas();
            Terminada = false;
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = _tab.RetirarPeca(origem);
            p.IncrementarQuantMovimentos();
            Peca pecaCapturada = _tab.RetirarPeca(destino);
            _tab.ColocarPeca(p, destino);
			if (pecaCapturada != null)
			{
				_capturadas.Add(pecaCapturada);
			}

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

		public HashSet<Peca> pecasCapturadas(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();
			foreach (Peca p in _capturadas)
			{
				if (p.CorPeca == cor)
				{
					aux.Add(p);
				}
			}
			return aux;
		}

		public HashSet<Peca> pecasEmJogo(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();
			foreach (Peca p in _pecas)
			{
				if (p.CorPeca == cor)
				{
					aux.Add(p);
				}
			}
			aux.ExceptWith(pecasCapturadas(cor));

			return aux;
		}

		public void ColocarNovaPeca(char coluna, int linha, Peca peca)
		{
			_tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			_pecas.Add(peca);
		}

        private void ColocarPecas()
        {
			ColocarNovaPeca('d', 7, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('c', 7, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('e', 7, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('c', 8, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('e', 8, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('d', 8, new Rei(_tab, Cor.Branca));
			ColocarNovaPeca('a', 8, new Torre(_tab, Cor.Branca));

			ColocarNovaPeca('a', 1, new Torre(_tab, Cor.Preta));
			ColocarNovaPeca('h', 1, new Torre(_tab, Cor.Preta));

        }

    }
}

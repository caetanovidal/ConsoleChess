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
		public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            _tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
			_pecas = new HashSet<Peca>();
			_capturadas = new HashSet<Peca>();
			Xeque = false;
            ColocarPecas();
            Terminada = false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = _tab.RetirarPeca(origem);
            p.IncrementarQuantMovimentos();
            Peca pecaCapturada = _tab.RetirarPeca(destino);
            _tab.ColocarPeca(p, destino);
			if (pecaCapturada != null)
			{
				_capturadas.Add(pecaCapturada);
			}

			// jogada especial roque pequeno

			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

				Peca T = _tab.RetirarPeca(origemT);
				T.IncrementarQuantMovimentos();
				_tab.ColocarPeca(T, destinoT);
			}

			// jogada especial roque grande

			if (p is Rei && destino.Coluna == origem.Coluna - 3)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

				Peca T = _tab.RetirarPeca(origemT);
				T.IncrementarQuantMovimentos();
				_tab.ColocarPeca(T, destinoT);
			}


			return pecaCapturada;
        }

		public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
		{
			Peca p = _tab.RetirarPeca(destino);
			p.DecrementarQuantMovimentos();
			if (pecaCapturada != null)
			{
				_tab.ColocarPeca(pecaCapturada, destino);
				_capturadas.Remove(pecaCapturada);
			}
			_tab.ColocarPeca(p, origem);


			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

				Peca T = _tab.RetirarPeca(destinoT);
				T.DecrementarQuantMovimentos();
				_tab.ColocarPeca(T, origemT);
			}

			// jogada especial roque grande

			if (p is Rei && destino.Coluna == origem.Coluna - 3)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

				Peca T = _tab.RetirarPeca(destinoT);
				T.DecrementarQuantMovimentos();
				_tab.ColocarPeca(T, origemT);
			}

		}

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			Peca PecaCapturada = ExecutaMovimento(origem, destino);

			if (EstaEmXeque(_jogadorAtual))
			{
				DesfazMovimento(origem, destino, PecaCapturada);
				throw new TabuleiroException("Voce nao pode se colocar em cheque");
			}

			if (EstaEmXeque(Adversaria(_jogadorAtual)))
			{
				Xeque = true;
			}
			else
			{
				Xeque = false;
			}

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

		private Cor Adversaria(Cor cor)
		{
			if (cor == Cor.Branca)
			{
				return Cor.Preta;
			}
			else
				return Cor.Branca;
		}

		private Peca Rei(Cor cor)
		{
			foreach (Peca p in pecasEmJogo(cor))
			{
				if (p is Rei)
				{
					return p;
				}
			}
			return null;
		}

		public bool EstaEmXeque(Cor cor)
		{
			Peca rei = Rei(cor);
			Posicao posRei = rei.PosicaoPeca;

			if (rei == null)
			{
				throw new TabuleiroException("Nao tem rei da cor " + cor + " no tabuleiro!");
			}

			foreach (Peca p in pecasEmJogo(Adversaria(cor)))
			{
				bool[,] mat = p.MovimentosPossiveis();

				if(mat[posRei.Linha, posRei.Coluna])
				{
					return true;
				}
			}
			return false;
		}


		public void ColocarNovaPeca(char coluna, int linha, Peca peca)
		{
			_tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			_pecas.Add(peca);
		}

        private void ColocarPecas()
        {
			
			ColocarNovaPeca('a', 8, new Torre(_tab, Cor.Branca));
			ColocarNovaPeca('h', 8, new Torre(_tab, Cor.Branca));

			ColocarNovaPeca('d', 8, new Rei(_tab, Cor.Branca, this));
			ColocarNovaPeca('e', 8, new Dama(_tab, Cor.Branca));


			ColocarNovaPeca('c', 8, new Bispo(_tab, Cor.Branca));
			ColocarNovaPeca('f', 8, new Bispo(_tab, Cor.Branca));

			ColocarNovaPeca('a', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('b', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('c', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('d', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('e', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('f', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('g', 7, new Peao(_tab, Cor.Branca));
			ColocarNovaPeca('h', 7, new Peao(_tab, Cor.Branca));

			ColocarNovaPeca('b', 8, new Cavalo(_tab, Cor.Branca));
			ColocarNovaPeca('g', 8, new Cavalo(_tab, Cor.Branca));





			ColocarNovaPeca('a', 1, new Torre(_tab, Cor.Preta));
			ColocarNovaPeca('h', 1, new Torre(_tab, Cor.Preta));

			ColocarNovaPeca('e', 1, new Rei(_tab, Cor.Preta, this));
			ColocarNovaPeca('d', 1, new Dama(_tab, Cor.Preta));


			ColocarNovaPeca('c', 1, new Bispo(_tab, Cor.Preta));
			ColocarNovaPeca('f', 1, new Bispo(_tab, Cor.Preta));

			ColocarNovaPeca('a', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('b', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('c', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('d', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('e', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('f', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('g', 2, new Peao(_tab, Cor.Preta));
			ColocarNovaPeca('h', 2, new Peao(_tab, Cor.Preta));

			ColocarNovaPeca('b', 1, new Cavalo(_tab, Cor.Preta));
			ColocarNovaPeca('g', 1, new Cavalo(_tab, Cor.Preta));





		}

	}
}

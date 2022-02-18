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
		public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            _tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
			_pecas = new HashSet<Peca>();
			_capturadas = new HashSet<Peca>();
			Xeque = false;
			VulneravelEnPassant = null;
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

			// jogada especial roque

			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
				Peca T = _tab.RetirarPeca(origemT);
				T.IncrementarQuantMovimentos();
				_tab.ColocarPeca(T, destinoT);
			}

			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca T = _tab.RetirarPeca(origemT);
				T.IncrementarQuantMovimentos();
				_tab.ColocarPeca(T, destinoT);
			}

			// jogada especial en passant

			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && pecaCapturada == null)
				{
					Posicao enPassant;

					if (p.CorPeca == Cor.Branca)
					{
						enPassant = new Posicao(destino.Linha - 1, destino.Coluna);
					}
					else
					{
						enPassant = new Posicao(destino.Linha + 1, destino.Coluna);
					}
					pecaCapturada = _tab.RetirarPeca(enPassant);
					_capturadas.Add(pecaCapturada);
				}
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

			// jogada especial roque

			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca T = _tab.RetirarPeca(destinoT);
				T.DecrementarQuantMovimentos();
				_tab.ColocarPeca(T, origemT);
			}

			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca T = _tab.RetirarPeca(destinoT);
				T.DecrementarQuantMovimentos();
				_tab.ColocarPeca(T, origemT);
			}

			// jogada especial en passant

			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
				{
					Peca peao = _tab.RetirarPeca(destino);
					Posicao PosP;
					if (p.CorPeca == Cor.Branca)
					{
						PosP = new Posicao(4, destino.Coluna);
					}
					else
					{
						PosP = new Posicao(3, destino.Coluna);
					}
					_tab.ColocarPeca(peao, PosP);
				}
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

			Peca p = _tab.PecaChamar(destino);

			// jogada especial promocao

			if (p is Peao)
			{
				if (p.CorPeca == Cor.Branca && (destino.Linha == 7) || p.CorPeca == Cor.Preta && (destino.Linha == 0))
				{
					p = _tab.RetirarPeca(destino);
					_pecas.Remove(p);

					Peca Dama = new Dama(_tab, p.CorPeca);
					_tab.ColocarPeca(Dama, destino);
					_pecas.Add(Dama);
				}
			}

			if (EstaEmXeque(Adversaria(_jogadorAtual)))
			{
				Xeque = true;
			}
			else
			{
				Xeque = false;
			}

			if (TesteXequeMate(Adversaria(_jogadorAtual)))
			{
				Terminada = true;

			}
			else
			{
				_turno++;
				MudaJogador();
			}

			

			// jogadaespecial en passant

			if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
			{
				VulneravelEnPassant = p;
			}
			else
			{
				VulneravelEnPassant = null;
			}
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

		public bool TesteXequeMate(Cor cor)
		{
			if (!EstaEmXeque(cor))
				return false;

			foreach (Peca p in pecasEmJogo(cor))
			{
				bool[,] mat = p.MovimentosPossiveis();
				for (int i=0; i < _tab.Linhas; i++)
				{
					for (int j=0; j < _tab.Colunas; i++)
					{
						if (i > 7 || j > 7)
						{
							break;
						}
						if (mat[i, j])
						{
							Posicao destino = new Posicao(i, j);
							Peca PecaCapturada = ExecutaMovimento(p.PosicaoPeca, destino);
							bool testeXeque = EstaEmXeque(cor);
							DesfazMovimento(p.PosicaoPeca, destino, PecaCapturada);
							if (!testeXeque)
								return false;
						}
					}
				}
			}
			return true;
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

			ColocarNovaPeca('d', 8, new Dama(_tab, Cor.Branca));
			ColocarNovaPeca('e', 8, new Rei(_tab, Cor.Branca, this));


			ColocarNovaPeca('c', 8, new Bispo(_tab, Cor.Branca));
			ColocarNovaPeca('f', 8, new Bispo(_tab, Cor.Branca));

			ColocarNovaPeca('a', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('b', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('c', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('d', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('e', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('f', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('g', 7, new Peao(_tab, Cor.Branca, this));
			ColocarNovaPeca('h', 7, new Peao(_tab, Cor.Branca, this));

			ColocarNovaPeca('b', 8, new Cavalo(_tab, Cor.Branca));
			ColocarNovaPeca('g', 8, new Cavalo(_tab, Cor.Branca));





			ColocarNovaPeca('a', 1, new Torre(_tab, Cor.Preta));
			ColocarNovaPeca('h', 1, new Torre(_tab, Cor.Preta));

			ColocarNovaPeca('e', 1, new Rei(_tab, Cor.Preta, this));
			ColocarNovaPeca('d', 1, new Dama(_tab, Cor.Preta));


			ColocarNovaPeca('c', 1, new Bispo(_tab, Cor.Preta));
			ColocarNovaPeca('f', 1, new Bispo(_tab, Cor.Preta));

			ColocarNovaPeca('a', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('b', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('c', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('d', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('e', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('f', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('g', 2, new Peao(_tab, Cor.Preta, this));
			ColocarNovaPeca('h', 2, new Peao(_tab, Cor.Preta, this));

			ColocarNovaPeca('b', 1, new Cavalo(_tab, Cor.Preta));
			ColocarNovaPeca('g', 1, new Cavalo(_tab, Cor.Preta));





		}

	}
}

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
        private int _turno;
        private Cor _jogadorAtual;
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

        private void ColocarPecas()
        {
			_tab.ColocarPeca(new Rei(_tab, Cor.Branca), new PosicaoXadrez('d', 8).ToPosicao());
			_tab.ColocarPeca(new Torre(_tab, Cor.Branca), new PosicaoXadrez('a', 8).ToPosicao());


			_tab.ColocarPeca(new Torre(_tab, Cor.Vermelha), new PosicaoXadrez('a', 1).ToPosicao());
            _tab.ColocarPeca(new Torre(_tab, Cor.Vermelha), new PosicaoXadrez('h', 1).ToPosicao());

        }

    }
}

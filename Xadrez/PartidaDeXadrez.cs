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
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('a', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('b', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('c', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('d', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('e', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('f', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('g', 2).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Vermelha), new PosicaoXadrez('h', 2).ToPosicao());

            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('a', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('b', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('c', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('d', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('e', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('f', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('g', 7).ToPosicao());
            _tab.ColocarPeca(new Peao(_tab, Cor.Branca), new PosicaoXadrez('h', 7).ToPosicao());

            _tab.ColocarPeca(new Torre(_tab, Cor.Vermelha), new PosicaoXadrez('a', 1).ToPosicao());
            _tab.ColocarPeca(new Torre(_tab, Cor.Vermelha), new PosicaoXadrez('h', 1).ToPosicao());

        }

    }
}

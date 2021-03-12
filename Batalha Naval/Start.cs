using System;
using Batalha_Naval.br.ufrpe.controller;
using Batalha_Naval.br.ufrpe.model;

namespace Batalha_Naval
{
    class Start
    {
        static void Main(string[] args)
        {
            ControladorTabuleiro controlller = new ControladorTabuleiro(new Tabuleiro());
            controlller.prepararjogo();
            controlller.executaJogo();
        }
    }
}

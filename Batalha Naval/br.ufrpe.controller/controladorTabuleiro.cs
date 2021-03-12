using System;
using System.Collections.Generic;
using System.Text;
using Batalha_Naval.br.ufrpe.model;

namespace Batalha_Naval.br.ufrpe.controller
{
    class ControladorTabuleiro
    {
        private Random r = new Random();

        private List<int> jogadasIAEixoX = new List<int>();
        private List<int> jogadasIAEixoY = new List<int>();
        private List<int> ultimasPosicaosBarcoAtingidasX = new List<int>();
        private List<int> ultimasPosicaosBarcoAtingidasY = new List<int>();
        private List<int> jogadasPlayerEixoX = new List<int>();
        private List<int> jogadasPlayerEixoY = new List<int>();

        private int countBarcosCPU = 5;
        private int countBarcosPlayer = 5;

        enum direcaoCerta { nada, x_Plus, x_Minus, y_Plus, y_Minus }
        direcaoCerta lastDir = direcaoCerta.x_Plus;

        private Tabuleiro tabCPU = new Tabuleiro();
        private Tabuleiro tabPlayer;

        private Submarino subCPU = new Submarino(true);
        private Encouracado encouraCPU = new Encouracado(true);
        private Encouracado encoura1CPU = new Encouracado(false);
        private NavioGuerra navioGCPU = new NavioGuerra(true);
        private PortaAviao portaCPU = new PortaAviao(true);

        private Submarino subPlayer = new Submarino(true);
        private Encouracado encouraPlayer = new Encouracado(true);
        private Encouracado encoura1Player = new Encouracado(false);
        private NavioGuerra navioGPlayer = new NavioGuerra(true);
        private PortaAviao portaPlayer = new PortaAviao(true);

        private List<Peca> barcosCPU = new List<Peca>();
        private List<Peca> barcosPlayer = new List<Peca>();


        public ControladorTabuleiro(Tabuleiro tabPlayer)
        {
            this.tabPlayer = tabPlayer;
        }

        public void prepararjogo()
        {
            tabPlayer = new Tabuleiro();
            addBarcosListCPU();
            addBarcosListPlayer();

            posicionarBarcos(tabCPU, barcosCPU);
            posicionarBarcos(tabPlayer, barcosPlayer);
        }

        private void addBarcosListCPU()
        {
            barcosCPU.Add(subCPU);
            barcosCPU.Add(encouraCPU);
            barcosCPU.Add(encoura1CPU);
            barcosCPU.Add(navioGCPU);
            barcosCPU.Add(portaCPU);
        }

        private void addBarcosListPlayer()
        {
            barcosPlayer.Add(subPlayer);
            barcosPlayer.Add(encouraPlayer);
            barcosPlayer.Add(encoura1Player);
            barcosPlayer.Add(navioGPlayer);
            barcosPlayer.Add(portaPlayer);
        }

        private void posicionarBarcos(Tabuleiro tab, List<Peca> barcos)
        {
            tab.getMatrizPecas()[5, 9] = barcos[0];

            tab.getMatrizPecas()[3, 9] = barcos[1];
            tab.getMatrizPecas()[4, 9] = barcos[1];
            tab.getMatrizPecas()[5, 9] = barcos[1];

            tab.getMatrizPecas()[9, 0] = barcos[2];
            tab.getMatrizPecas()[9, 1] = barcos[2];
            tab.getMatrizPecas()[9, 2] = barcos[2];

            tab.getMatrizPecas()[1, 5] = barcos[3];
            tab.getMatrizPecas()[1, 6] = barcos[3];
            tab.getMatrizPecas()[1, 7] = barcos[3];
            tab.getMatrizPecas()[1, 8] = barcos[3];

            tab.getMatrizPecas()[9, 4] = barcos[4];
            tab.getMatrizPecas()[9, 5] = barcos[4];
            tab.getMatrizPecas()[9, 6] = barcos[4];
            tab.getMatrizPecas()[9, 7] = barcos[4];
            tab.getMatrizPecas()[9, 8] = barcos[4];
            tab.getMatrizPecas()[9, 9] = barcos[4];
        }

        public void executaJogo()
        {
            int x, y;
            bool jogadaPlayer = true, jogadaCPU = false;

            while (true)
            {
                if (jogadaPlayer)
                {
                    Console.WriteLine("Digite a coordenada X");
                    x = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Digite a coordenada Y");
                    y = Int32.Parse(Console.ReadLine());

                    jogadaPlayer = jogarPlayer(x, y);
                    if (jogadaPlayer)
                    {
                        if (getCountBarcosCPU() == 0)
                        {
                            Console.WriteLine("Parabéns, o player ganhou");
                            break;
                        }
                    }
                    else
                    {
                        jogadaCPU = true;
                        Console.WriteLine("Errou.Vez da CPU");
                    }
                }
                else if (jogadaCPU)
                {
                    jogadaCPU = jogarCPU();

                    if (jogadaCPU)
                    {
                        if (getCountBarcosPlayer() == 0)
                        {
                            Console.WriteLine("Parabéns, a cpu ganhou");
                            break;
                        }
                    }
                    else
                    {
                        jogadaPlayer = true;
                        Console.WriteLine("Errou.Vez do Jogador");
                    }
                }
            }
        }

        private bool jogarCPU()
        {
            int eixoX = 0;
            int eixoY = 0;
            bool acertou = false;

            while (true)
            {
                if (jogadasIAEixoX.Count == 0)
                {
                    eixoX = r.Next(2, 8);
                    eixoY = r.Next(2, 7);
                }
                else
                {
                    eixoX = r.Next(0, 10);
                    eixoY = r.Next(0, 10);
                }

                if (ultimasPosicaosBarcoAtingidasX.Count > 0)
                {
                    eixoX = ultimasPosicaosBarcoAtingidasX[ultimasPosicaosBarcoAtingidasX.Count-1];
                    eixoY = ultimasPosicaosBarcoAtingidasY[ultimasPosicaosBarcoAtingidasY.Count-1];

                    if (lastDir.Equals(direcaoCerta.x_Plus))
                    {
                        if (eixoX+1 < 10 && !tabPlayer.getMatrizPecas()[eixoX + 1, eixoY].getAtingido())
                        {
                            eixoX++;
                        }
                        else
                        {
                            lastDir = direcaoCerta.x_Minus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.x_Minus))
                    {
                        if (eixoX-1 >= 0 && !tabPlayer.getMatrizPecas()[eixoX - 1, eixoY].getAtingido())
                        {
                            eixoX--;
                        }
                        else
                        {
                            lastDir = direcaoCerta.y_Plus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.y_Plus))
                    {
                        if (eixoX+1 < 10 && !tabPlayer.getMatrizPecas()[eixoX, eixoY + 1].getAtingido())
                        {
                            eixoY++;
                        }
                        else
                        {
                            lastDir = direcaoCerta.y_Minus;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.y_Minus))
                    {
                        if (eixoX-1 >= 0 && !tabPlayer.getMatrizPecas()[eixoX, eixoY - 1].getAtingido())
                        {
                            eixoY--;
                        }
                    }
                    if (lastDir.Equals(direcaoCerta.nada))
                    {
                        ultimasPosicaosBarcoAtingidasX.RemoveRange(1, ultimasPosicaosBarcoAtingidasX.Count-1);
                        ultimasPosicaosBarcoAtingidasY.RemoveRange(1, ultimasPosicaosBarcoAtingidasY.Count-1);
                    }
                }

                bool checkPosicaoTiro = true;
                for (int i = 0; i < jogadasIAEixoX.Count; i++)
                {
                    if (jogadasIAEixoX[i] == eixoX && jogadasIAEixoY[i] == eixoY)
                    {
                        checkPosicaoTiro = false;
                        break;
                    }
                }

                if (!tabPlayer.getMatrizPecas()[eixoX, eixoY].getAtingido() && checkPosicaoTiro)
                {
                    break;
                }
                else
                {
                    if (lastDir.Equals(direcaoCerta.y_Minus))
                    {
                        lastDir = direcaoCerta.nada;
                    }else{
                        lastDir++;
                    }
                }
            }

            Console.WriteLine("CPU:" + eixoX + " - " + eixoY);

            Peca barco = tabPlayer.getMatrizPecas()[eixoX, eixoY];
            int tamanhoBarco = barco.getTamanho();

            jogadasIAEixoX.Add(eixoX);
            jogadasIAEixoY.Add(eixoY);

            if (barco.GetType() != typeof(Agua))
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY].setTamanho(tamanhoBarco - 1);
                ultimasPosicaosBarcoAtingidasX.Add(eixoX);
                ultimasPosicaosBarcoAtingidasY.Add(eixoY);
                acertou = true;
                Console.WriteLine("CPU acertou barco na posição X:" + eixoX + " e Y:" + eixoY);
            }
            else
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY].setAtingido(true);
                acertou = false;
            }

            if (tabPlayer.getMatrizPecas()[eixoX, eixoY].getTamanho() <= 0)
            {
                tabPlayer.getMatrizPecas()[eixoX, eixoY].setAtingido(true);
                countBarcosPlayer -= 1;
                ultimasPosicaosBarcoAtingidasX.Clear();
                ultimasPosicaosBarcoAtingidasY.Clear();
                lastDir = direcaoCerta.x_Plus;
                Console.WriteLine("Afundou barco do jogador");
            }
            return acertou;
        }

        private bool jogarPlayer(int x, int y)
        {

            bool acertou = false;

            Peca barco = tabCPU.getMatrizPecas()[x, y];
            int tamanhoBarco = barco.getTamanho();

            jogadasPlayerEixoX.Add(x);
            jogadasPlayerEixoY.Add(y);

            if (barco.GetType() != typeof(Agua))
            {
                tabCPU.getMatrizPecas()[x, y].setTamanho(tamanhoBarco - 1);
                acertou = true;
                Console.WriteLine("Jogador acertou barco na posição X:" + x + " e Y:" + y);
            }
            else if (barco.getAtingido())
            {
                acertou = true;
                Console.WriteLine("Você já jogou nesse lugar. Escolha outra posição");
            }
            else
            {
                tabCPU.getMatrizPecas()[x, y].setAtingido(true);
                acertou = false;
            }

            if (tabCPU.getMatrizPecas()[x, y].getTamanho() <= 0)
            {
                tabCPU.getMatrizPecas()[x, y].setAtingido(true);
                countBarcosCPU -= 1;
                Console.WriteLine("Afundou barco da CPU");
            }
            return acertou;
        }

        public int getCountBarcosCPU()
        {
            return this.countBarcosCPU;
        }

        public int getCountBarcosPlayer()
        {
            return this.countBarcosPlayer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Batalha_Naval.br.ufrpe.model
{
    public class Quadrado
    {
        private int condenadaY;//será o número da coluna(ex:"1")
        private int condenadaX;//será a letra da linha(ex:"1")
        private string cor;//inizializando com branco, acertou na agua muda pra azul e se acertou no barco muda pra vermelho
        private bool preenchido;//se o quadrado está preenchido com barco
        public Quadrado(int condenadaX, int condenadaY)
        {
            this.condenadaY = condenadaY;
            this.condenadaX = condenadaX;
            this.cor = escolherCor.Branco.ToString();
            this.preenchido = false;
        }
        public int getCordenadaX()
        {
            return this.condenadaX;
        }
        public int getCordenadaY()
        {
            return this.condenadaY;
        }
        public string getCor()
        {
            return this.cor;
        }
        public bool getPreenchido()
        {
            return this.preenchido;
        }

        public void setPreenchido(bool preenchido)
        {
            this.preenchido = preenchido;
        }
        public void setCor(string novaCor)
        {
            this.cor = novaCor;
        }
        public enum escolherCor { Branco, Vermelho, Azul };

        public override string ToString()
        {
            return "CordenadaX: " + this.condenadaX + "\nCordenadaY" + this.condenadaY + "\nCor" + this.cor +
                    "\nPreenchido com barco" + this.preenchido;
        }
    }
}
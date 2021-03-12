﻿using System;
using System.Collections.Generic;
using System.Text;


namespace Batalha_Naval.br.ufrpe.model//submarino,gerra,encouraçado,portaAviao
{
    public abstract class Peca
    {
        private bool orientacao;//true vertical, false horizontal
        private bool atingido;//true se a peca foi atingida e falso se nao
        private int tamanho;//quantas casas ocupa
        private int id;
        protected Peca(bool orientacao)
        {
            this.orientacao = orientacao;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }
       
        public Boolean getOrientacao()
        {
            return this.orientacao;
        }

        public bool getAtingido()
        {
            return this.atingido;
        }
        public int getTamanho()
        {
            return this.tamanho;
        }
        public void setTamanho(int tamanho)
        {
            this.tamanho = tamanho;
        }
        
        public void setAtingido(bool status)
        {
            this.atingido=status;
        }

        public override string ToString()
        {
            return "Orientacao: " + this.orientacao+ "\nPeça atacada:"+this.atingido
                + "\ntamanho:"+this.tamanho;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyukovyProgram.pages.Hry
{
    class Pexeso
    {
        private int pocetHracichKaret = 51;//celkovy pocet hracich karet, hodnota v promenne odpovida max hodnote CASE ve funkci vratCestuObrazku

        int[,] hraciPole;
        public int sirka;
        public int vyska;

        private int sourSirka; //souradnice sirky karty na kterou bylo kliknuto
        private int sourVyska; //souradnice vysky karty na kterou bylo kliknuto

        public int step;//krok hry: 1-otoč první kartu, 2-otoč druhou kartu,3-porovnej karty, 4 - pokud jsou shodne zneviditelnit, jinak otoč obě karty zpět
        int prvniKarta;
        public int sourSirkaPrvni;
        public int sourVyskaPrvni;
        public int sourSirkaDruha;
        public int sourVyskaDruha;
        int druhaKarta;
        bool shoda;

        private int pocetHracu;
        private int hrac;
        private int[] hracSkore;

        private int pocetShod;



        public Pexeso(int sirka, int vyska, int pocetHracu)
        {
            this.sirka = sirka;
            this.vyska = vyska;
            hraciPole = new int[sirka, vyska];
            vytvorHraciPole(sirka, vyska);
            zamichejHraciPole();
            step = 1;
            pocetShod = 0;
            nastavPocetHracu(pocetHracu);
            vylosujHrace();
        }

        private void nastavPocetHracu(int pocetHracu)
        {
            this.pocetHracu = pocetHracu;
            hracSkore = new int[pocetHracu];
        }

        public int vratPocetHracu() {
            return this.pocetHracu;
        }

        private void vylosujHrace()
        {
            Random random = new Random();
            hrac = random.Next(0, pocetHracu);
        }

        public void dalsiHrac()
        {
            //if (!porovnejKarty())
                hrac++;
            if (hrac >= pocetHracu)
                hrac = 0;
        }

        public int cisloHrace()
        {
            return hrac;
        }

        public void prictiBodHraci()
        {
            hracSkore[hrac]++;
        }

        public int pocetBodu(int hrac)
        {
            return hracSkore[hrac];
        }

        public void krokHry()
        {
            if (step == 1)
            {
                prvniKarta = hraciPole[sourSirka, sourVyska];
                sourSirkaPrvni = sourSirka;
                sourVyskaPrvni = sourVyska;
                step++;
            }
            else if (step == 2 && jeDruhaKartaRozdilna())
            {
                druhaKarta = hraciPole[sourSirka, sourVyska];
                sourSirkaDruha = sourSirka;
                sourVyskaDruha = sourVyska;
                step++;
            }
            else if(step == 3)
            {
                step++;
            }
            if (step >= 4)
                step = 1;
        }

        public bool jeDruhaKartaRozdilna()
        {
            if (sourSirka != sourSirkaPrvni || sourVyska != sourVyskaPrvni)
                return true;
            else
                return false;
        }

        public void incShoda()
        {
            pocetShod++;
        }

        public bool porovnejKarty()
        {
            if (prvniKarta == druhaKarta)
            {
                shoda = true;
                
            }
            else
                shoda = false;
            return shoda;
        }

        public bool jeKonecHry()
        {
            if (pocetShod >= (sirka * vyska) / 2)
                return true;
            else
                return false;
        }

        private void vytvorHraciPole(int sirka, int vyska)
        {
            Random random = new Random();
            int pocetObrazku = ((sirka * vyska) / 2) + random.Next(1, (pocetHracichKaret - ((sirka * vyska) / 2)) + 1);
            //pocet karticek je dan sirka*vyska, jelikoz jsou vzdy dve karty stejne tak deleno 2
            //aby byly použity pro každou hru jina sada karticek (ze vsech poskytnutych karticek) - je cislo zacinajici karty posunuto o náhodnou pozici v celem rozsahu karet


            int stejneObrazky = 2;

            //random.Next(1,(pocetHracichKaret - ((sirka * vyska) / 2))+1);
            for (int i = 0; i<sirka; i++)
            {
                for(int j = 0; j<vyska; j++)
                {
                    
                    if (stejneObrazky <= 0) { 
                        stejneObrazky = 2;
                        pocetObrazku--;
                    }
                    
                    stejneObrazky--;
                    hraciPole[i, j] = pocetObrazku;
                }
            }
        }



        /// <summary>
        /// funkce k promichani hraciho pole po jeho vytvoreni
        /// </summary>
        private void zamichejHraciPole()
        {
            Random random = new Random();
            int karta, nahodneCisloSirka, nahodneCisloVyska;

            for (int i = 0; i < this.sirka; i++)
            {
                for (int j = 0; j < this.vyska; j++)
                {//promichani pole s cisly otazek
                    nahodneCisloSirka = random.Next(this.sirka);//generuje nahodna cislo v rozsahu 0 až sirka-1
                    nahodneCisloVyska = random.Next(this.vyska);//generuje nahodna cislo v rozsahu 0 až vyska-1

                    karta = hraciPole[i, j];
                    hraciPole[i, j] = hraciPole[nahodneCisloSirka, nahodneCisloVyska];
                    hraciPole[nahodneCisloSirka, nahodneCisloVyska] = karta;
                }
            }
        }



        public int getCisloKarty(int sourSirka, int sourVyska)
        {
            this.sourSirka = sourSirka;
            this.sourVyska = sourVyska;
            return hraciPole[sourSirka, sourVyska];
        }

        public string vratCestuObrazku(int cisloObrazku)
        {
            String s = "";
            switch (cisloObrazku)
            {
                case 1:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_1.jpg";
                    break;
                case 2:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_2.jpg";
                    break;
                case 3:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_3.jpg";
                    break;
                case 4:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_4.jpg";
                    break;
                case 5:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_5.jpg";
                    break;
                case 6:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_6.jpg";
                    break;
                case 7:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_7.jpg";
                    break;
                case 8:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_8.jpg";
                    break;
                case 9:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_9.jpg";
                    break;
                case 10:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_10.jpg";
                    break;
                case 11:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_11.jpg";
                    break;
                case 12:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_12.jpg";
                    break;
                case 13:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_13.jpg";
                    break;
                case 14:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_14.jpg";
                    break;
                case 15:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_15.jpg";
                    break;
                case 16:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_16.jpg";
                    break;
                case 17:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_17.jpg";
                    break;
                case 18:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_18.jpg";
                    break;
                case 19:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_19.jpg";
                    break;
                case 20:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_20.jpg";
                    break;
                case 21:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_21.jpg";
                    break;
                case 22:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_22.jpg";
                    break;
                case 23:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_23.jpg";
                    break;
                case 24:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_24.jpg";
                    break;
                case 25:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_25.jpg";
                    break;
                case 26:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_26.jpg";
                    break;
                case 27:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_27.jpg";
                    break;
                case 28:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_28.jpg";
                    break;
                case 29:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_29.jpg";
                    break;
                case 30:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_30.jpg";
                    break;
                case 31:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_31.jpg";
                    break;
                case 32:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_32.jpg";
                    break;
                case 33:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_33.jpg";
                    break;
                case 34:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_34.jpg";
                    break;
                case 35:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_35.jpg";
                    break;
                case 36:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_36.jpg";
                    break;
                case 37:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_37.jpg";
                    break;
                case 38:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_38.jpg";
                    break;
                case 39:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_39.jpg";
                    break;
                case 40:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_40.jpg";
                    break;
                case 41:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_41.jpg";
                    break;
                case 42:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_42.jpg";
                    break;
                case 43:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_43.jpg";
                    break;
                case 44:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_44.jpg";
                    break;
                case 45:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_45.jpg";
                    break;
                case 46:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_46.jpg";
                    break;
                case 47:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_47.jpg";
                    break;
                case 48:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_48.jpg";
                    break;
                case 49:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_49.jpg";
                    break;
                case 50:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_50.jpg";
                    break;
                case 51:
                    s = @"\img\05_Hry\Pexeso_karticky\Karta_10x10cm_51.jpg";
                    break;
                
            }
            return s;
        }
    }
}

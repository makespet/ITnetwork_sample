using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VyukovyProgram.pages.Testy;

namespace VyukovyProgram
{
    public class TestCl
    {

        private int pocetOtazek;
        private int cisloOtazky; //ukazatel do pole cislaOtazek
        private int aktualniOtazka = 0;//uklada cislo posledni nezodpovezene otazky, cislo dosazene otazky
        private int vybranaMoznost;//cislo vybrane odpovedi

        public string[] otazky; //otazky
        public string[,] moznosti; //moznosti k otazce
        public int[] spravnaOdpoved;
        public int[] zvolenaOdpoved;
        public bool[] spravnostOdpovedi;

        private int[] cislaOtazek;//pole s nahodne rozlosovanymi cisly otazek

        public TestCl(int pocetOtazek)
        {
            this.pocetOtazek = pocetOtazek;
            otazky = new string[pocetOtazek];
            moznosti = new string[pocetOtazek, 4];
            spravnaOdpoved = new int[pocetOtazek];
            zvolenaOdpoved = new int[pocetOtazek];
            spravnostOdpovedi = new bool[pocetOtazek];
            cislaOtazek = new int[pocetOtazek];

            rozlosujOtazky();

        }

        public void startNovyTest()
        {
            cisloOtazky = 0;
            aktualniOtazka = 0;
        }

        public void dalsiOtazka()
        {
            cisloOtazky++;
            if (aktualniOtazka < cisloOtazky)
                aktualniOtazka = cisloOtazky;
        }

        public void predchoziOtazka()
        {
            if (cisloOtazky >= 1)
                cisloOtazky--;
        }

        public bool isActual()
        {
            if (cisloOtazky == aktualniOtazka)
                return true;
            else
                return false;
        }

        /// <summary>
        /// zdali je dana otazka posledni
        /// </summary>
        /// <returns></returns>
        public bool isLastQuest()
        {
            if (cisloOtazky >= pocetOtazek-1)
                return true;
            else
                return false;
        }

        public bool isFirstQuest()
        {
            if (cisloOtazky == 0)
                return true;
            else
                return false;
        }

        public int getcheckedAnswer()
        {
            return zvolenaOdpoved[cisloOtazky];
        }

        public bool getCheckedCorectness()
        {
            return spravnostOdpovedi[cisloOtazky];
        }

        public bool getPreviousCheckedCorectness()
        {
            return spravnostOdpovedi[cisloOtazky-1];
        }
        /// <summary>
        /// vraci pocet otazek v testu
        /// </summary>
        /// <returns></returns>
        public int getNumberOfQuestions()
        {
            return pocetOtazek;
        }

        /// <summary>
        /// cislo dalsi nahodne tazene otazky
        /// </summary>
        /// <returns></returns>
        public int getNextQuestionNumber()
        {
            return cislaOtazek[this.cisloOtazky];
        }


        /// <summary>
        /// nastavi test na posledni nezodpovezenou otazku
        /// </summary>
        public void setActualQuestion()
        {
            cisloOtazky = aktualniOtazka;
        }

        /// <summary>
        /// zapise do instancni promenne cislo vybrane moznosti uzivatelem
        /// </summary>
        /// <param name="vybranaMoznost"></param>
        public void zvolMoznost(int vybranaMoznost)
        {
            this.vybranaMoznost = vybranaMoznost;
            zvolenaOdpoved[cisloOtazky] = vybranaMoznost;
            vyhodnotOdpoved();
        }

        /// <summary>
        /// vyhodnoti spravnost odpovedi a zapise do pole spravnostOdpovedi
        /// </summary>
        /// <returns></returns>
        private bool vyhodnotOdpoved()
        {
            if (zvolenaOdpoved[cisloOtazky] == spravnaOdpoved[cislaOtazek[this.cisloOtazky]])
                spravnostOdpovedi[cisloOtazky] = true;
            else
                spravnostOdpovedi[cisloOtazky] = false;

            return spravnostOdpovedi[cisloOtazky];
        }

        
        /// <summary>
        /// funkce vraci pocet nespravnych odpovedi v testu
        /// </summary>
        /// <returns></returns>
        public int pocetSpravnychOdpovedi()
        {
            int j = 0;
            
            for (int i = 0; i < pocetOtazek; i++)
            {
                if (spravnostOdpovedi[i] == true)
                    j++;
            }
            return j;
        }

        /// <summary>
        /// funkce vraci pocet nespravnych odpovedi v testu
        /// </summary>
        /// <returns></returns>
        public int pocetNespravnychOdpovedi()
        {
            int j = 0;
            
            for (int i = 0; i < pocetOtazek; i++)
            {
                if (spravnostOdpovedi[i] == false)
                    j++;
            }
            return j;
        }

        /// <summary>
        /// funkce na rozlosovani poradi otazek
        /// </summary>
        private void rozlosujOtazky()
        {
            
            Random random = new Random();
            int nahodneCislo; //nahodne cislo pro zamichani pole s cisly otazek
            int cisloOtazky; //pomocna promenna pro prohozeni dvou prvku pole s cisly otazek

            for (int i = 0; i < pocetOtazek; i++)
            {//naplneni pole s cisly otazek cisly od 1 do poctu otazek
                cislaOtazek[i] = i;
            }

            for (int i = 0; i < pocetOtazek; i++)
            {//promichani pole s cisly otazek
                cisloOtazky = cislaOtazek[i];
                nahodneCislo = random.Next(0, pocetOtazek);
                cislaOtazek[i] = cislaOtazek[nahodneCislo];
                cislaOtazek[nahodneCislo] = cisloOtazky;
            }
        }

        /// <summary>
        /// funkce na vlozeni otazky do testu
        /// </summary>
        /// <param name="cisloOtazky"></param>
        /// <param name="otazka"></param>
        /// <param name="odpoved1"></param>
        /// <param name="odpoved2"></param>
        /// <param name="odpoved3"></param>
        /// <param name="odpoved4"></param>
        /// <param name="cisloSpravneOdpovedi"></param>
        public void vytvorOtazku(String otazka, String odpoved1, String odpoved2, String odpoved3, String odpoved4, int cisloSpravneOdpovedi)
        {
            otazky[this.cisloOtazky] = otazka;
            moznosti[this.cisloOtazky, 0] = String.Format(odpoved1);
            moznosti[this.cisloOtazky, 1] = String.Format(odpoved2);
            moznosti[this.cisloOtazky, 2] = String.Format(odpoved3);
            moznosti[this.cisloOtazky, 3] = String.Format(odpoved4);
            spravnaOdpoved[this.cisloOtazky] = cisloSpravneOdpovedi;
            this.cisloOtazky++;
        }


        /// <summary>
        /// slouzi k vypisu c. otazky z celkoveho poctu otazek
        /// </summary>
        /// <returns></returns>
        public string cisloOtazkyZeVsech()
        {
            return String.Format((cisloOtazky+1)+"/"+pocetOtazek );
        }

        
    }
}


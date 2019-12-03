using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hra___DU
{
    class GameLogic
    {
        Random rand = new Random();
        public string[] balicek { get; set; }

        public string[] hrac1_balicek { get; set; }
        public string[] hrac2_balicek { get; set; }

        public string hraciKarta { get; set; }


        public string[] barva =
{
            "Srdce",
            "Piky",
            "Krize",
            "Kary"
        };


        public void StartGame()
        {
            
           

            Vygeneruj();
            Rozdej();

            /* DEBUG
            for (int k = 0; k < 44; k++)
            {
                MessageBox.Show(gamelogic.balicek[k]);
            }
            */


            // srdce = h
            // kara = d
            // piky = s
            // krize = c
        }


        private string[] hodnoty =
{
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "J",
            "Q",
            "K",
            "A"
        };

        public void reshuffle(string[] texts)
        {
           
            for (int t = 0; t < texts.Length; t++)
            {
                string tmp = texts[t];
                int r = rand.Next(t, texts.Length);
                texts[t] = texts[r];
                texts[r] = tmp;
            }


        }

        public string[] RemoveFromBalicek(string CardToRemove, string[] Deck)
        {
            int removeIndex = Array.IndexOf(Deck, CardToRemove);

            if (removeIndex >= 0)
            {

                string[] newStrItems = new string[Deck.Length - 1];


                for (int i = 0, j = 0; i < newStrItems.Length; i++, j++)
                {

                    if (i == removeIndex)
                    {
                        j++;
                    }


                    newStrItems[i] = Deck[j];
                }


                return newStrItems;
            }
            else return Deck;
        }

        public string[] AddToBalicek(string CardToAdd, string[] Deck)
        {

            Array.Resize(ref Deck, Deck.Length + 1);
            Deck[Deck.GetUpperBound(0)] = CardToAdd;



            
            
            return Deck;
                     
        }

        public string[] Liznout(string[] Deck)
        {

            if (balicek.Length != 0)
            {
                Deck = AddToBalicek(balicek.Last(), Deck);
                balicek = RemoveFromBalicek(balicek.Last(), balicek);
                return Deck;
            }
            else
            {
                 balicek = Vygeneruj();
                /*
                 foreach(string karta in hrac1_balicek)
                 {
                    balicek = RemoveFromBalicek(karta, Deck);
                 }
                foreach (string karta in hrac2_balicek)
                {
                    balicek = RemoveFromBalicek(karta, Deck);
                }
                */
                Deck = AddToBalicek(balicek.Last(), Deck);
                return Deck;
            }
        }

        public string[] Vygeneruj()
        {
            int index = 0;
            balicek = new string[52];

            for (int i = 0; i < barva.Length; i++)
            {
                for (int k = 0; k < hodnoty.Length; k++)
                {
                    balicek[index] = barva[i] + " " + hodnoty[k];
                    index++;
                }
            }
            reshuffle(balicek);
            return balicek;
        }

        public void Rozdej()
        {
            hrac1_balicek = new string[4];
            hrac2_balicek = new string[4];

            Array.Clear(hrac1_balicek,0,hrac1_balicek.Length - 1);
            Array.Clear(hrac1_balicek,0 , hrac2_balicek.Length - 1);
            for (int i = 0; i < 4; i++)
            {
              


                hrac1_balicek[i] = balicek[rand.Next(0, balicek.Length)];
                balicek = RemoveFromBalicek(hrac1_balicek[i],balicek);
                hrac2_balicek[i] = balicek[rand.Next(0, balicek.Length)];
                balicek = RemoveFromBalicek(hrac2_balicek[i], balicek);

            }

            hraciKarta = balicek[rand.Next(0, balicek.Length)];
            balicek = RemoveFromBalicek(hraciKarta, balicek);




        }

        public void HracNaTahu(Form1 form)
        {
            /*for (int i =0; i<Deck.Length; i++)
            {
                form.Zobraz(hrac1_balicek);

                if (Deck[i].Image != null)
                {
                    Deck[i].Enabled = true;
                }

                
            }
            */
            form.Zobraz(hrac1_balicek);
        }

        public void RobotNaTahu(Form1 form)
        {

            if (form.eso)
            {
                form.eso = false;
                MessageBox.Show(form.jmenoOponenta + " stoji! Na tahu jsi ty.");
                HracNaTahu(form);
            }
            else
            {
                bool zahral = false;
                bool menic = false;
                int beresDve = form.beresDve;
                bool eso = form.eso;
                foreach (string karta in hrac2_balicek)
                {
                    if (Tah(karta, out menic, ref beresDve, ref eso))
                    {

                        hrac2_balicek = RemoveFromBalicek(karta, hrac2_balicek);
                        if (menic)
                        {
                            string meniNa = barva[rand.Next(0, 4)];
                            form.ZobrazTah(meniNa);

                            MessageBox.Show(form.jmenoOponenta + " zahral kartu " + karta + " a meni na barvu " + meniNa);
                            zahral = true;
                            form.beresDve = beresDve;
                            form.eso = eso;
                            break;
                        }
                        else
                        {
                            form.ZobrazTah(karta);
                            MessageBox.Show(form.jmenoOponenta + " zahral kartu " + karta);
                            zahral = true;
                            form.beresDve = beresDve;
                            form.eso = eso;
                            break;
                        }

                    }
                }
                if (!zahral)
                {
                    if (beresDve > 0)
                    {
                        MessageBox.Show(form.jmenoOponenta + " si liznul " + beresDve + " karty");
                        for (int i = 0; i < beresDve; i++)
                        {
                            hrac2_balicek = Liznout(hrac2_balicek);
                        }
                        form.beresDve = 0;

                    }
                    else
                    {
                        MessageBox.Show(form.jmenoOponenta + " si liznul");
                        form.beresDve = beresDve;
                        hrac2_balicek = Liznout(hrac2_balicek);
                    }

                };
                HracNaTahu(form);
            }
           
        }

        public bool Tah(string karta, out bool menic, ref int beresDve, ref bool eso)
        {
            //MessageBox.Show("Tah" + karta);
            String[] karta_strip = karta.Split(' ');
            String[] hraci_karta_strip = hraciKarta.Split(' ');

            menic = false;

            if (hraci_karta_strip.Length == 1)
            {
                if (karta_strip[0] == hraci_karta_strip[0])
                {
                    return true;
                }
                else return false;
            }
            else
            {
                if (karta_strip[1] == "Q")
                {
                    if (hraci_karta_strip[1] == "7") return false;
                    else
                    {
                        menic = true;
                        return true;
                    }

                }
                else if (hraci_karta_strip[1] == "A")
                {
                    if (eso)
                    {
                        return false;
                    }
                    else
                    {
                        if (karta_strip[0] == hraci_karta_strip[0])
                        {
                            if (karta_strip[1] == "7")
                            {
                                beresDve += 2;
                            }
                            if (karta_strip[1] == "A")
                            {
                                eso = true;
                            }

                            return true;
                        }
                        if (karta_strip[1] == hraci_karta_strip[1])
                        {
                            if (karta_strip[1] == "7")
                            {
                                beresDve += 2;
                            }
                            if (karta_strip[1] == "A")
                            {
                                eso = true;
                            }

                            return true;
                        }
                    }
                    return false;

                }
                else if (hraci_karta_strip[1] == "7")
                {
                    if (beresDve > 0)
                    {
                        if (karta_strip[1] == "7")
                        {
                            beresDve += 2;
                            return true;

                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (karta_strip[0] == hraci_karta_strip[0])
                        {
                            if (karta_strip[1] == "7")
                            {
                                beresDve += 2;
                            }
                            if (karta_strip[1] == "A")
                            {
                                eso = true;
                            }

                            return true;
                        }
                        if (karta_strip[1] == hraci_karta_strip[1])
                        {
                            if (karta_strip[1] == "7")
                            {
                                beresDve += 2;
                            }
                            if (karta_strip[1] == "A")
                            {
                                eso = true;
                            }

                            return true;
                        }
                        else return false;
                    }
                   

                }
                else if (karta_strip[1] == hraci_karta_strip[1])
                {
                    if (karta_strip[1] == "7")
                    {
                        beresDve += 2;
                    }
                    if (karta_strip[1] == "A")
                    {
                        eso = true;
                    }

                    return true;
                    
                }
                else if (karta_strip[0] == hraci_karta_strip[0])
                {
                    if (karta_strip[1] == "7")
                    {
                        beresDve += 2;
                        
                    }
                    if (karta_strip[1] == "A")
                    {
                        eso = true;
                    }
                    return true;
                    
                }
                else return false;
            }

           

        }

    }
}

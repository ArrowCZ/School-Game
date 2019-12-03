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


    public partial class Form1 : Form
    {


        public string jmenoHrace;
        public string jmenoOponenta;
        public int beresDve { get; set; } = 0;
        public bool eso { get; set; } = false;
        public Form1()
        {
            InitializeComponent();
        }

        UserInput userinput;
        GameLogic gamelogic;
        Random rand;

        private void kartyBack_Click(object sender, EventArgs e)
        {
            if (beresDve > 0)
            {
                for (int i =0; i < beresDve; i++)
                {
                    gamelogic.hrac1_balicek = gamelogic.Liznout(gamelogic.hrac1_balicek);
                }
                Zobraz(gamelogic.hrac1_balicek);
                MessageBox.Show("Liznul sis " + beresDve +" karty");
                DisableCards();
                beresDve = 0;
                if (gamelogic.hrac1_balicek.Length > 0)
                {
                    gamelogic.RobotNaTahu(this);
                }
            }
            else
            {
                gamelogic.hrac1_balicek = gamelogic.Liznout(gamelogic.hrac1_balicek);
                Zobraz(gamelogic.hrac1_balicek);
                MessageBox.Show("Liznul sis kartu");
                DisableCards();
                if (gamelogic.hrac1_balicek.Length > 0)
                {
                    gamelogic.RobotNaTahu(this);
                }
            }

        }

        
        public void Zobraz(string[] balicek)
        {

            if (eso)
            {
                eso = false;
                MessageBox.Show("Stojis! Na tahu je " + jmenoOponenta);
                DisableCards();
                gamelogic.RobotNaTahu(this);
            }
           else  if (gamelogic.hrac1_balicek.Length == 0)
            {
                label5.Text = "Počet karet: " + gamelogic.balicek.Length.ToString();
                label2.Text = gamelogic.hrac1_balicek.Length.ToString() + " karty";
                label3.Text = gamelogic.hrac2_balicek.Length.ToString() + " karty";

                int hraciKartaIndex = imageList1.Images.IndexOfKey(gamelogic.hraciKarta + ".png");
                pictureBox5.Image = imageList1.Images[hraciKartaIndex];
                pictureBox5.Tag = gamelogic.hraciKarta;
                MessageBox.Show("Vyhral jsi!");
                DisableCards();
                textBox1.Enabled = true;
                button1.Enabled = true;

            }
            else if (gamelogic.hrac2_balicek.Length == 0)
            {
                label5.Text = "Počet karet: " + gamelogic.balicek.Length.ToString();
                label2.Text = gamelogic.hrac1_balicek.Length.ToString() + " karty";
                label3.Text = gamelogic.hrac2_balicek.Length.ToString() + " karty";

                int hraciKartaIndex = imageList1.Images.IndexOfKey(gamelogic.hraciKarta + ".png");
                pictureBox5.Image = imageList1.Images[hraciKartaIndex];
                pictureBox5.Tag = gamelogic.hraciKarta;
                MessageBox.Show(jmenoOponenta + " vyhral!");
                DisableCards();
                textBox1.Enabled = true;
                button1.Enabled = true;

            }
            else
            {
                PictureBox[] playerBoxes = {
               Karta1,
               Karta2,
               Karta3,
               Karta4,
               Karta5,
               Karta6,
               Karta7,
               Karta8,
               Karta9,
               Karta10,
               Karta11,
               Karta12,
               Karta13,
               Karta14
             };

                foreach (PictureBox karta in playerBoxes)
                {
                    karta.Image = null;
                    karta.Tag = null;
                }


                for (int i = 0; i < balicek.Length; i++)
                {
                    //MessageBox.Show(balicek[i]);
                    int cardToDisplay = imageList1.Images.IndexOfKey(balicek[i] + ".png");
                    playerBoxes[i].Image = imageList1.Images[cardToDisplay];
                    playerBoxes[i].Tag = balicek[i];
                    playerBoxes[i].Enabled = true;

                }
                kartyBack.Enabled = true;
                label5.Text = "Počet karet: " + gamelogic.balicek.Length.ToString();
                label2.Text = gamelogic.hrac1_balicek.Length.ToString() + " karty";
                label3.Text = gamelogic.hrac2_balicek.Length.ToString() + " karty";

                int hraciKartaIndex = imageList1.Images.IndexOfKey(gamelogic.hraciKarta + ".png");
                pictureBox5.Image = imageList1.Images[hraciKartaIndex];
                pictureBox5.Tag = gamelogic.hraciKarta;
            }


           
           

        }

        public void ZobrazTah(string karta)
        {
            
            gamelogic.hraciKarta = karta;
            Zobraz(gamelogic.hrac1_balicek);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userinput = new UserInput();
            gamelogic = new GameLogic();
            rand = new Random();

            label_menic.Hide();
            radioButtonSrdce.Hide();
            radioButtonKary.Hide();
            radioButtonKrize.Hide();
            radioButtonPiky.Hide();
            button_menic.Hide();
            DisableCards();
        }

        public void WaitForInput()
        {
            MessageBox.Show("Zahral jsi menice, prosim vyber barvu");

            label_menic.Show();
            radioButtonSrdce.Show();
            radioButtonKary.Show();
            radioButtonKrize.Show();
            radioButtonPiky.Show();
            button_menic.Show();

            DisableCards();

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string[] jmena =
            {
                "Laďa",
                "Martin",
                "Jirka Hrnek",
                "Aleš",
                "Liboros",
                "Angi",
                "Majsner",
                "Starej Brunda",
                "Psychopat",
                "Pacička",
                "Drahoš",
                "Roky",
                "Ivona"
            };


            PictureBox[] playerBoxes = {
               Karta1,
               Karta2,
               Karta3,
               Karta4,
               Karta5,
               Karta6,
               Karta7,
               Karta8,
               Karta9,
               Karta10,
               Karta11,
               Karta12,
               Karta13,
               Karta14
             };


            jmenoHrace = textBox1.Text;
            label1.Text = jmenoHrace;
            jmenoOponenta = jmena[rand.Next(0, jmena.Length)];
            label4.Text = jmenoOponenta;
            textBox1.Enabled = false;
            button1.Enabled = false;

            gamelogic.StartGame();
            kartyBack.Image = imageList1.Images[44];

            /*Hraci Karta*/

            MessageBox.Show("Hra začíná! Oponentem ti je " + label4.Text);
            gamelogic.HracNaTahu(this);
        }

        public void DisableCards()
        {

            PictureBox[] playerBoxes = {
               Karta1,
               Karta2,
               Karta3,
               Karta4,
               Karta5,
               Karta6,
               Karta7,
               Karta8,
               Karta9,
               Karta10,
               Karta11,
               Karta12,
               Karta13,
               Karta14
             };

            for (int i = 0; i < playerBoxes.Length; i++)
            {
                playerBoxes[i].Enabled = false;
            }
            kartyBack.Enabled = false;
        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Karta_MouseEnter(object sender, EventArgs e)
        {
            PictureBox HoveredBox = (PictureBox)sender;
           // MessageBox.Show(HoveredBox.Name);


            if (HoveredBox.Tag != null)
            {

                switch (HoveredBox.Name)
                {
                    case "Karta1": { Karta1Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta2": { Karta2Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta3": { Karta3Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta4": { Karta4Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta5": { Karta5Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta6": { Karta6Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta7": { Karta7Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta8": { Karta8Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta9": { Karta9Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta10": { Karta10Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta11": { Karta11Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta12": { Karta12Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta13": { Karta13Label.Text = HoveredBox.Tag.ToString(); break; }
                    case "Karta14": { Karta14Label.Text = HoveredBox.Tag.ToString(); break; }

                }

            }


        }

        private void Karta_MouseLeave(object sender, EventArgs e)
        {
            PictureBox HoveredBox = (PictureBox)sender;

            switch (HoveredBox.Name)
            {
                case "Karta1": { Karta1Label.Text = ""; break; }
                case "Karta2": { Karta2Label.Text = ""; break; }
                case "Karta3": { Karta3Label.Text = ""; break; }
                case "Karta4": { Karta4Label.Text = ""; break; }
                case "Karta5": { Karta5Label.Text = ""; break; }
                case "Karta6": { Karta6Label.Text = ""; break; }
                case "Karta7": { Karta7Label.Text = ""; break; }
                case "Karta8": { Karta8Label.Text = ""; break; }
                case "Karta9": { Karta9Label.Text = ""; break; }
                case "Karta10": { Karta10Label.Text = ""; break; }
                case "Karta11": { Karta11Label.Text = ""; break; }
                case "Karta12": { Karta12Label.Text = ""; break; }
                case "Karta13": { Karta13Label.Text = ""; break; }
                case "Karta14": { Karta14Label.Text = ""; break; }
            }

        }

        private void Karta_Click(object sender, EventArgs e)
        {
            PictureBox ClickedBox = (PictureBox)sender;
            bool menic = false;
            int beresDve = this.beresDve;
            bool eso = this.eso;
            if (ClickedBox.Tag != null)
            {
                if (gamelogic.Tah(ClickedBox.Tag.ToString(),out menic,ref beresDve,ref eso))
                {
                    if (menic)
                    {
                        MessageBox.Show("Zahral jsi kartu " + ClickedBox.Tag.ToString());
                        gamelogic.hrac1_balicek = gamelogic.RemoveFromBalicek(ClickedBox.Tag.ToString(), gamelogic.hrac1_balicek);
                        this.eso = eso;
                        this.beresDve = beresDve;
                        WaitForInput();

                    }
                    else
                    {
                       
                        MessageBox.Show("Zahral jsi kartu " + ClickedBox.Tag.ToString());
                        gamelogic.hrac1_balicek = gamelogic.RemoveFromBalicek(ClickedBox.Tag.ToString(), gamelogic.hrac1_balicek);
                        ZobrazTah(ClickedBox.Tag.ToString());
                        DisableCards();
                        this.beresDve = beresDve;
                        this.eso = eso;
                        if (gamelogic.hrac1_balicek.Length > 0)
                        {
                            gamelogic.RobotNaTahu(this);
                        }
                        
                    }


                }

            }
        }

        private void Button_menic_Click(object sender, EventArgs e)
        {
            label_menic.Hide();
            radioButtonSrdce.Hide();
            radioButtonKary.Hide();
            radioButtonKrize.Hide();
            radioButtonPiky.Hide();
            button_menic.Hide();

            string meniNa = "Srdce";

            if (radioButtonSrdce.Checked) meniNa = "Srdce";
            else if (radioButtonKary.Checked) meniNa = "Kary";
            else if (radioButtonKrize.Checked) meniNa = "Krize";
            else if (radioButtonPiky.Checked) meniNa = "Piky";

            ZobrazTah(meniNa);
            MessageBox.Show("Menis na barvu " + meniNa);
            DisableCards();
            if (gamelogic.hrac1_balicek.Length > 0)
            {
                gamelogic.RobotNaTahu(this);
            }
        }
    }
}

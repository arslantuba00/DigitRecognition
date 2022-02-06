using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace DigitRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rastgele = new Random();

        List<double> weights = new List<double>();
        List<double> weights2 = new List<double>();
        List<double> bias = new List<double>();
        List<double> normalize = new List<double>();
        List<double> sLO = new List<double>();
        List<double> lLO = new List<double>();
        List<double> ciktiHatalari = new List<double>();
        List<double> araHatalari = new List<double>();
        List<double> accuracy = new List<double>();

        float ogrenmeHizi = 0.1f;
        float momentum = 0.1f;
        int sifir, bir, iki, uc, dort, bes, alti, yedi, sekiz, dokuz;

        int iteration = 10;
        int ilkKatmanNode = 784;//28*28 piksel
        int gizliKatmanNode = 10;
        int cikisKatmanNode = 10;
        float[] gercekCiktiDegerlerSifir = new float[10]; float[] gercekCiktiDegerlerBir = new float[10]; float[] gercekCiktiDegerlerIki = new float[10];

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 12;
            dataGridView2.ColumnCount = 12;
            dataGridView1.Columns[0].Name = "Rakam Sınıfı";
            for (int k = 1; k <= 10; k++)
            {
                dataGridView1.Columns[k].Name = k.ToString();
                if (k == 10) { dataGridView1.Columns[k].Name = "0"; }
            }

            dataGridView1.Columns[11].Name = "Ortalama";

            for (int k = 1; k <= 10; k++)
            {
                dataGridView2.Columns[k].Name = k.ToString();
                if (k == 10) { dataGridView2.Columns[k].Name = "0"; }
            }

            dataGridView2.Columns[11].Name = "Ortalama";
        }

        float[] gercekCiktiDegerlerUc = new float[10]; float[] gercekCiktiDegerlerDort = new float[10]; float[] gercekCiktiDegerlerBes = new float[10];
        float[] gercekCiktiDegerlerAlti = new float[10]; float[] gercekCiktiDegerlerYedi = new float[10]; float[] gercekCiktiDegerlerSekiz = new float[10];
        float[] gercekCiktiDegerlerDokuz = new float[10];

      


        private void button1_Click(object sender, EventArgs e)
        {
            for (int g = 0; g < 10; g++)
            {
                gercekCiktiDegerlerSifir[g] = 0; gercekCiktiDegerlerBir[g] = 0; gercekCiktiDegerlerIki[g] = 0; gercekCiktiDegerlerUc[g] = 0; gercekCiktiDegerlerDort[g] = 0;
                gercekCiktiDegerlerBes[g] = 0; gercekCiktiDegerlerAlti[g] = 0; gercekCiktiDegerlerYedi[g] = 0; gercekCiktiDegerlerSekiz[g] = 0; gercekCiktiDegerlerDokuz[g] = 0;
            }
            
            for (int p = 0; p < 10; p++)
            {
                for (int s = 0; s < 28; s++)
                {
                    switch (p)
                    {
                        case 0:
                            gercekCiktiDegerlerSifir[9] = 0.9f;
                            break;
                        case 1:
                            gercekCiktiDegerlerBir[0] = 0.9f;
                            break;
                        case 2:
                            gercekCiktiDegerlerIki[1] = 0.9f;
                            break;
                        case 3:
                            gercekCiktiDegerlerUc[2] = 0.9f;
                            break;
                        case 4:
                            gercekCiktiDegerlerDort[3] = 0.9f;
                            break;
                        case 5:
                            gercekCiktiDegerlerBes[4] = 0.9f;
                            break;
                        case 6:
                            gercekCiktiDegerlerAlti[5] = 0.9f;
                            break;
                        case 7:
                            gercekCiktiDegerlerYedi[6] = 0.9f;
                            break;
                        case 8:
                            gercekCiktiDegerlerSekiz[7] = 0.9f;
                            break;
                        case 9:
                            gercekCiktiDegerlerDokuz[8] = 0.9f;
                            break;

                    }
                }
            }


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            Color okunanRenk;
            double R = 0.0;
            Bitmap girisResmi;

            for (int b = 1; b < 2000; b++)
            {
                girisResmi = new Bitmap(@path + "\\TrainSet\\" + b.ToString() + ".png");

                int resimGenisligi = Convert.ToInt32(girisResmi.Width);
                int resimYuksekligi = Convert.ToInt32(girisResmi.Height);

                int sayac = 0;
                
                for (int x = 0; x < resimGenisligi; x++)
                {
                    for (int y = 0; y < resimYuksekligi; y++)
                    {
                        okunanRenk = girisResmi.GetPixel(x, y);
                        R = okunanRenk.R;


                        sayac++;
                        /*textBox1.Text += okunanRenk.ToString();*//*R.ToString() + "*" + G.ToString() + "*" + B.ToString()+"???"+ sayac.ToString()+"???"+"\n";*/

                        normalize.Add((double)(R) / (double)255.0);
                        //textBox1.Text += normalize[y].ToString() + "????" + sayac.ToString() + "    ";
                    }
                }

                for (int iterasyon = 0; iterasyon < iteration; iterasyon++)
                {

                    for (int t = 0; t < ilkKatmanNode; t++)
                    {
                        for (int r = 0; r < gizliKatmanNode; r++)
                        {
                            weights.Add(rastgele.NextDouble() * (1 - (-1)) + (-1));//*(max-min)+min
                                                                                   //textBox1.Text += weights[r].ToString();
                        }

                    }

                    int temp = 0;
                    double sum = 0.0;
                    for (int m = 1; m <= gizliKatmanNode; m++)
                    {
                        for (int z = 0; z < ilkKatmanNode; z++)
                        {
                            for (int t = temp; t < temp + gizliKatmanNode; t += gizliKatmanNode)
                            {

                                sum += weights[t] * normalize[z];

                            }
                            temp = temp + gizliKatmanNode;
                        }
                        temp = m;
                        sLO.Add((1 / (1 + Math.Exp(-sum))));//second Layer Outputs
                        sum = 0.0;
                    }
                    temp = 0;
                    //textBox1.Text += sLO[0].ToString() + "      " + sLO[1].ToString();

                    for (int t = 0; t < gizliKatmanNode; t++)
                    {
                        for (int r = 0; r < cikisKatmanNode; r++)
                        {
                            weights2.Add(rastgele.NextDouble() * (1 - (-1)) + (-1));//*(max-min)+min
                                                                                    //textBox1.Text += weights[r].ToString();
                        }

                    }


                    for (int m = 1; m <= cikisKatmanNode; m++)
                    {
                        for (int z = 0; z < gizliKatmanNode; z++)
                        {
                            for (int t = temp; t < temp + cikisKatmanNode; t += cikisKatmanNode)
                            {

                                sum += weights2[t] * sLO[z];

                            }
                            temp = temp + cikisKatmanNode;
                        }
                        temp = m;
                        lLO.Add((1 / (1 + Math.Exp(-sum))));//last Layer Outputs
                        sum = 0.0;
                    }
                    temp = 0;
                    //textBox1.Text += lLO[0].ToString() + "      " + lLO[1].ToString() + "      " + lLO[9].ToString();

                    
                    sifir = bir = iki = uc = dort = bes = alti = yedi = sekiz = dokuz = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (lLO[i] >= gercekCiktiDegerlerBir[i]) { bir++; }//0.9 0 0 0 0 0 0 0 0 0
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerBir[i] - lLO[i])); }
                                break;
                            case 1:
                                if (lLO[i] >= gercekCiktiDegerlerIki[i]) { iki++; }//0 0.9 0 0 0 0 0 0 0 0
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerIki[i] - lLO[i])); }
                                break;
                            case 2:
                                if (lLO[i] >= gercekCiktiDegerlerUc[i]) { uc++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerUc[i] - lLO[i])); }
                                break;
                            case 3:
                                if (lLO[i] >= gercekCiktiDegerlerDort[i]) { dort++; }
                                break;
                            case 4:
                                if (lLO[i] >= gercekCiktiDegerlerBes[i]) { bes++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerDort[i] - lLO[i])); }
                                break;
                            case 5:
                                if (lLO[i] >= gercekCiktiDegerlerAlti[i]) { alti++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerBes[i] - lLO[i])); }
                                break;
                            case 6:
                                if (lLO[i] >= gercekCiktiDegerlerYedi[i]) { yedi++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerAlti[i] - lLO[i])); }
                                break;
                            case 7:
                                if (lLO[i] >= gercekCiktiDegerlerSekiz[i]) { sekiz++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerYedi[i] - lLO[i])); }
                                break;
                            case 8:
                                if (lLO[i] >= gercekCiktiDegerlerDokuz[i]) { dokuz++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerSekiz[i] - lLO[i])); }
                                break;
                            case 9:
                                if (lLO[i] >= gercekCiktiDegerlerSifir[i]) { sifir++; }
                                else { ciktiHatalari.Add((lLO[i] * (1 - lLO[i])) * (gercekCiktiDegerlerDokuz[i] - lLO[i])); }
                                break;

                        }
                    }

                   




                    for (int h = 0; h < gizliKatmanNode; h++)
                    {
                        araHatalari.Add((1 - sLO[h]) * sLO[h] * (ciktiHatalari[h]));
                    }



                    for (int t = 0; t < gizliKatmanNode; t++)
                    {
                        for (int l = temp; l < temp + cikisKatmanNode; l++)
                        {
                            weights2[l] = weights2[l] + (ogrenmeHizi * ciktiHatalari[l]) * (sLO[t]);
                        }
                        temp += 10;
                    }
                    temp = 0;

                    for (int t = 0; t < ilkKatmanNode; t++)
                    {
                        for (int l = temp; l < temp + gizliKatmanNode; l++)
                        {
                            weights[l] = weights[l] + (ogrenmeHizi * araHatalari[l]) * (normalize[t]);
                        }
                        temp += 10;
                    }


                }
                
            }
            accuracy.Add((double)(bir * 100) / (double)(200));
            accuracy.Add((double)(iki * 100) / (double)(200));
            accuracy.Add((double)(uc * 100) / (double)(200));
            accuracy.Add((double)(dort * 100) / (double)(200));
            accuracy.Add((double)(bes * 100) / (double)(200));
            accuracy.Add((double)(alti * 100) / (double)(200));
            accuracy.Add((double)(yedi * 100) / (double)(200));
            accuracy.Add((double)(sekiz * 100) / (double)(200));
            accuracy.Add((double)(dokuz * 100) / (double)(200));
            accuracy.Add((double)(sifir * 100) / (double)(200));

            dataGridView1.Rows.Add(accuracy[0], accuracy[1], accuracy[2], accuracy[3], accuracy[4], accuracy[5], accuracy[6], accuracy[7], accuracy[8], accuracy[9]);

            accuracy.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            Color okunanRenk;
            double R = 0.0;
            Bitmap girisResmi;

            for (int b = 1; b < 2000; b++)
            {
                girisResmi = new Bitmap(@path + "\\TestSet\\" + b.ToString() + ".png");

                int resimGenisligi = Convert.ToInt32(girisResmi.Width);
                int resimYuksekligi = Convert.ToInt32(girisResmi.Height);

                int sayac = 0;
                
                for (int x = 0; x < resimGenisligi; x++)
                {
                    for (int y = 0; y < resimYuksekligi; y++)
                    {
                        okunanRenk = girisResmi.GetPixel(x, y);
                        R = okunanRenk.R;
                        sayac++;

                        normalize.Add((double)(R) / (double)255.0);

                    }
                }


                int temp = 0;
                double sum = 0.0;
                for (int m = 1; m <= gizliKatmanNode; m++)
                {
                    for (int z = 0; z < ilkKatmanNode; z++)
                    {
                        for (int t = temp; t < temp + gizliKatmanNode; t += gizliKatmanNode)
                        {

                            sum += weights[t] * normalize[z];

                        }
                        temp = temp + gizliKatmanNode;
                    }
                    temp = m;
                    sLO.Add((1 / (1 + Math.Exp(-sum))));//second Layer Outputs
                    sum = 0.0;
                }
                temp = 0;


                for (int m = 1; m <= cikisKatmanNode; m++)
                {
                    for (int z = 0; z < gizliKatmanNode; z++)
                    {
                        for (int t = temp; t < temp + cikisKatmanNode; t += cikisKatmanNode)
                        {

                            sum += weights2[t] * sLO[z];

                        }
                        temp = temp + cikisKatmanNode;
                    }
                    temp = m;
                    lLO.Add((1 / (1 + Math.Exp(-sum))));//last Layer Outputs
                    sum = 0.0;
                }
                temp = 0;

                sifir = bir = iki = uc = dort = bes = alti = yedi = sekiz = dokuz = 0;
                for (int i = 0; i < 10; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (lLO[i] >= gercekCiktiDegerlerBir[i]) { bir++; textBox1.Text="1 "; }//0.9 0 0 0 0 0 0 0 0 0
                            
                            break;
                        case 1:
                            if (lLO[i] >= gercekCiktiDegerlerIki[i]) { iki++; textBox1.Text = "2 "; }//0 0.9 0 0 0 0 0 0 0 0
                            
                            break;
                        case 2:
                            if (lLO[i] >= gercekCiktiDegerlerUc[i]) { uc++; textBox1.Text = "3 "; }
                            
                            break;
                        case 3:
                            if (lLO[i] >= gercekCiktiDegerlerDort[i]) { dort++; textBox1.Text = "4 "; }
                            break;
                        case 4:
                            if (lLO[i] >= gercekCiktiDegerlerBes[i]) { bes++; textBox1.Text = "5 "; }
                           
                            break;
                        case 5:
                            if (lLO[i] >= gercekCiktiDegerlerAlti[i]) { alti++; textBox1.Text = "6 "; }
                           
                            break;
                        case 6:
                            if (lLO[i] >= gercekCiktiDegerlerYedi[i]) { yedi++; textBox1.Text = "7 "; }
                            
                            break;
                        case 7:
                            if (lLO[i] >= gercekCiktiDegerlerSekiz[i]) { sekiz++; textBox1.Text = "8 "; }

                           
                            break;
                        case 8:
                            if (lLO[i] >= gercekCiktiDegerlerDokuz[i]) { dokuz++; textBox1.Text = "9 "; }
                            
                            break;
                        case 9:
                            if (lLO[i] >= gercekCiktiDegerlerSifir[i]) { sifir++; textBox1.Text = "0 "; }
                            
                            break;

                    }
                }

                accuracy.Add((double)(bir * 100) / (double)(200));
                accuracy.Add((double)(iki * 100) / (double)(200));
                accuracy.Add((double)(uc * 100) / (double)(200));
                accuracy.Add((double)(dort * 100) / (double)(200));
                accuracy.Add((double)(bes * 100) / (double)(200));
                accuracy.Add((double)(alti * 100) / (double)(200));
                accuracy.Add((double)(yedi * 100) / (double)(200));
                accuracy.Add((double)(sekiz * 100) / (double)(200));
                accuracy.Add((double)(dokuz * 100) / (double)(200));
                accuracy.Add((double)(sifir * 100) / (double)(200));

                dataGridView2.Rows.Add(accuracy[0], accuracy[1], accuracy[2], accuracy[3], accuracy[4], accuracy[5], accuracy[6], accuracy[7], accuracy[8], accuracy[9]);

                accuracy.Clear();
            }
        }

    }
}


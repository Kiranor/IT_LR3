using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using CipherLib ;

namespace Form
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private int _key = 2;
        private static List<string> result = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void BruteForce()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);

            
            int key = 0;
            string phrase;
            bool found = false;
      
            Stopwatch myWatch = new Stopwatch();
            
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                phrase = sr.ReadLine();
            }

            myWatch.Start();
            while (!found)
            {
                if (Cipher.Caesar(phrase, 'd', key) == Cipher.Label)
                {
                    found = true;
                }
                else key++;
            }
            myWatch.Stop();
            result.Add(key.ToString());
            result.Add(myWatch.ElapsedMilliseconds.ToString());
        }
       private void button1_Click(object sender, EventArgs e)
       {
           _key = int.Parse(textBox1.Text) % 128;
       }
       //TODO Сделать отображение файла
       //Засшифровка обычным шифром
       private void button2_Click(object sender, EventArgs e)
       {
           var fileDialog = new OpenFileDialog();
           fileDialog.ShowDialog();
           var path = Path.GetFullPath(fileDialog.FileName);

           Cipher.Code(path, _key);
           MessageBox.Show("Файл успешно зашифрован");
       }
       
        //Расшифровка обычным шифром
        private void button3_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);
            
            Cipher.Decode(path, _key);
            MessageBox.Show("Файл успешно расшифрован");
        }

        //Засшифровка улучшенным шифром
        private void button2_Click_1(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);

            Cipher.Code(path, true, _key);
            MessageBox.Show("Файл успешно расшифрован");
        }
        
        //Расшифровка улучшенным шифром
        private void button5_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);
            
            Cipher.Decode(path, true,_key);
            MessageBox.Show("Файл успешно расшифрован");
        }

        //TODO Сделать перебор в отдельном потоке
        //Подбор ключа
        private void button6_Click(object sender, EventArgs e)
        {
            result.Clear();

            //Thread t = new Thread(new ThreadStart(delegate { BruteForce(); }));
            //t.Start(); t.Join();
            BruteForce();
            textBox2.Text = "Найденный ключ: " + result[0] + " Подбор занял " + result[1] + " мc.";
        }
    }
}
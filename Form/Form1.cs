using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

//using Cipher ;

namespace Form
{

    //TODO Собрать в библиотеку
    public static class Cipher
    {
        private const string Label = "Encrypted";

        private static string Caesar(string line, char mode, int key = 2)
        {
            string encLine = null;
            
            if (mode == 'e') 
                foreach (var letter in line) 
                    encLine += (char) ((letter + key) % 256);
            else 
                foreach (var letter in line) 
                    encLine += (char) ((letter - key) % 256);

            return encLine;
        }

        private static string Better_Caesar(string line, char mode, int key = 2)
        {
            string encLine = null;
            int count = 0;
            
            if (mode == 'e')
            {
                foreach (var letter in line)
                {
                    if (count++ % 2 == 0) 
                        encLine += (char) ((letter + key) % 256);
                    else encLine += (char) 
                        ((letter + key/2) % 256);
                }
            }
            else
            {
                foreach (var letter in line)
                {
                    if (count++ % 2 == 0) 
                        encLine += (char) ((letter - key) % 256);
                    else 
                        encLine += (char) ((letter - key/2) % 256);
                }
            }

            return encLine;
        }
        //TODO Сделать правильное добавление лейбла в файл
        public static void Code(string fileName, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null) 
                    text.Add(Caesar(line, 'e', key));
            }
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine(Caesar(Label, 'e', key));
                foreach (var line in text) 
                    sw.WriteLine(line);
            }
        }

//TODO Исправить ошибку с лейблом при дешифровке
        public static void Decode(string fileName, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                if (sr.ReadLine() != Caesar(Label, 'd', key))
                {
                    //throw new Exception("Файл не может быть расшифрован.");
                }
                while ((line = sr.ReadLine()) != null) text.Add(Caesar(line, 'd', key));
            }
            using (var sw = new StreamWriter(fileName))
            {
                foreach (var line in text)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
    public partial class Form1 : System.Windows.Forms.Form
    {
        //private string _path = @"D:\USATU\3 Курс\Информационные технологии\LR3\Test.txt";
        public Form1()
        {
            InitializeComponent();
        }
        
       private void button1_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);
            Console.Write(path);
            
            //Cipher myCipher = new Cipher();
            //Cipher.Code(path);
            //Cipher.Decode(path);
        }
       //TODO Сделать отображение файла
       //Засшифровка
       private void button2_Click(object sender, EventArgs e)
       {
           textBox1.Clear();
           
           var fileDialog = new OpenFileDialog();
           fileDialog.ShowDialog();
           var path = Path.GetFullPath(fileDialog.FileName);
           Console.Write(path);
            
           Cipher.Code(path);
           //textBox1.Text = @"Файл успешно зашифрован.";
       }
       

        //Расшифровка
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var path = Path.GetFullPath(fileDialog.FileName);
            Console.Write(path);

            Cipher.Decode(path);
            //textBox2.Text = @"Файл успешно расшифрован.";
        }
        
    }
}
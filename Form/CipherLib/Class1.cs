using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CipherLib
{
    public static class Cipher
    {
        public const string Label = "Encrypted";

        /// <summary>
        /// Шифрует или расшифровывает строку классичесим шифром Цезаря
        /// </summary>
        /// <param name="line"></param>
        /// <param name="mode"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Caesar(string line, char mode, int key = 2)
        {
            string encLine = null;
            
            if (mode == 'e') 
                foreach (var letter in line) 
                    encLine += (char) ((letter + key) % 256);
            else
            {
                foreach (var letter in line)
                    encLine += (char) ((letter - key) % 256);
            }
            return encLine;
        }

        /// <summary>
        /// Шифрует или расшифровывает строку улучшенным шифром Цезаря
        /// </summary>
        /// <param name="line"></param>
        /// <param name="mode"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Better_Caesar(string line, char mode, int key = 2)
        {
            string encLine = null;
            int count = 0;
            
            if (mode == 'e')
            {
                foreach (var letter in line)
                {
                    if (count++ % 2 == 0) 
                        encLine += (char) ((letter + key) % 256);
                    else 
                        encLine += (char) ((letter + key/2) % 256);
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
        /// <summary>
        /// Шифрует файл классическим шифров Цезаря
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="key"></param>
        //TODO Сделать правильное добавление лейбла в файл
        public static void Code(string fileName, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == Caesar(Label, 'e', key))
                    {
                        throw new Exception("Файл уже зашифрован.");
                    }
                    text.Add(Caesar(line, 'e', key));
                }
            }
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine(Caesar(Label, 'e', key));
                foreach (var line in text) 
                    sw.WriteLine(line);
            }
        }
        /// <summary>
        /// Шифрует файл улучшенным шифром Цезаря
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="better"></param>
        /// <param name="key"></param>
        public static void Code(string fileName, bool better, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null) 
                {
                    if (line == Better_Caesar(Label, 'e', key))
                    {
                        throw new Exception("Файл уже зашифрован.");
                    }
                    text.Add(Better_Caesar(line, 'e', key));
                }
            }
            using (var sw = new StreamWriter(fileName))
            {
                sw.WriteLine(Better_Caesar(Label, 'e', key));
                foreach (var line in text) 
                    sw.WriteLine(line);
            }
        }

//TODO Исправить ошибку с лейблом при дешифровке
        /// <summary>
        /// Расшифровывает файл классическим шифром Цезаря
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="key"></param>
        /// <exception cref="Exception"></exception>
        public static void Decode(string fileName, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                if (sr.ReadLine() != Caesar(Label, 'e', key))
                {
                    throw new Exception("Файл не может быть расшифрован.");
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
        /// <summary>
        /// Расшифровывает файл улучшенным шифром Цезаря
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="better"></param>
        /// <param name="key"></param>
        public static void Decode(string fileName, bool better, int key = 2)
        {
            var text = new List<string>();
            
            using (var sr = new StreamReader(fileName, Encoding.Default))
            {
                string line;
                if (sr.ReadLine() != Better_Caesar(Label, 'e', key))
                {
                    throw new Exception("Файл не может быть расшифрован.");
                }
                while ((line = sr.ReadLine()) != null) text.Add(Better_Caesar(line, 'd', key));
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
}
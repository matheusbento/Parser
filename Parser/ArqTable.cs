using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parser
{
    class ArqTable
    {
        private string[] lines;

        public void matriz(string[,] table)
        {
            int i = 0;
            foreach (string s in lines)
            {
                string[] substrings = s.Split(';');
                
                for (int j=0; j < contColum(); j++)
                {
                    table[i, j] = substrings[j];
                }
                i++;
            }
        }
        public void lerArquivo(String arquivo)
        {
            lines = System.IO.File.ReadAllLines(@arquivo);

        }
        public int contLine()
        {
            return lines.Length;
        }
        public int contColum()
        {
            int numColum = 0;
            foreach (char s in lines[0])
            {
                if (s == ';') numColum++;
            }
            return numColum+1;
        }
    }
}

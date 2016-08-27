using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Reflection.Emit;
using GraphVizWrapper;
using GraphVizWrapper.Queries;
using GraphVizWrapper.Commands;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            var wrapper = new GraphGeneration(getStartProcessQuery, getProcessStartInfoQuery, registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph G {a -> b; b -> c; c -> a;}", Enums.GraphReturnType.Png);

            /*
            ArqTable ar = new ArqTable();
            ar.lerArquivo("Table.txt");
            string[,] table = new string[ar.contLine(), ar.contColum()];
            ar.matriz(table);
            PredictiveAnalysis analysis = new PredictiveAnalysis("(i)*(i)$");
            analysis.inicioAnalise(table);
            Console.WriteLine("\n");
            Console.WriteLine("\\\\\\\\\\---------- TABELA --------\\\\\\\\\\");
            for (int i=0; i < ar.contLine(); i++)
            {
                for(int j=0; j < ar.contColum(); j++)
                {
                    Console.Write(table[i, j] + " \t");
                }
                Console.WriteLine();
            }*/
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class PredictiveAnalysis
    {
        Stack<String> pilha = new Stack<String>();
        String input;
        public PredictiveAnalysis(String input) {
            this.input = input;
        }
        public void inicioAnalise(string[,] table) {
            try
            {
                Console.WriteLine("ENTRADA -> "+input);
                pilha.Push(table[1, 0]);
                
                foreach (char caracter in input)
                {
                    if (caracter == '$') {
                        pilha.Push("$");
                        imprimirPilha();
                    }
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Caracter da entrada -> "+caracter);
                    Console.WriteLine("----------------------------------");
                    String aux = "";
                    
                    do{
                        //Console.WriteLine("TAMANHO PILHA" + pilha.Count);
                        if (pilha.Count > 0)
                        {
                            //Console.WriteLine("ENTRO");
                            aux = pilha.First();
                            Console.WriteLine("\n------------------------");
                            if (aux.Any(c => char.IsUpper(c)))
                                Console.WriteLine(" NÃO TERMINAL -> " + aux);
                            if (aux.Any(c => char.IsLower(c)))
                                Console.WriteLine(" TERMINAL -> " + aux);
                            Console.WriteLine("------------------------");
                            Console.WriteLine(" DESEMPILHA -> "+pilha.First());
                            pilha.Pop() ;
                            imprimirPilha();
                            Console.WriteLine("------------------------");
                        }
                        if (aux.Any(c => char.IsUpper(c)))
                        {
                            int posNaoTerm = posicaoNaoTerminal(table, aux);
                            int posTerminal = posicaoTerminal(table, caracter);
                            String derivacao = table[posNaoTerm, posTerminal];
                            if (derivacao == "error")
                            {
                                throw new Exception("Syntax Error onde a derivação é igual a '" + derivacao + "'");
                            }
                            else if (derivacao == "synch") {

                                throw new Exception("Nao sei oq fazer");
                            }
                            dividirString(derivacao);
                        }
                        else if (aux.Any(c => char.IsLower(c)) || aux.Any(c => char.IsPunctuation(c)) || aux.Any(c=> char.IsSymbol(c)))
                        {
                            if (aux == caracter.ToString())
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine(" -- MATCH('"+aux+"')");
                                Console.WriteLine("-------------------");
                                //pilha.Pop();
                                imprimirPilha();
                                if (aux == "$" && caracter == '$') {
                                    Console.WriteLine("****SUCESSO****");
                                }
                                //continue;
                                
                            }
                            else
                            {
                                throw new Exception("Syntax Error onde na pilha esta o simbolo '"+aux+"' e na entrada esta o simbolo '"+caracter+"'");
                            }
                        }
                    } while (aux.Any(c => char.IsUpper(c)));
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        public int posicaoNaoTerminal(String[,] vet, String letra)
        {
            for (int i = 0; i < vet.GetUpperBound(0)+1; i++)
            {
                if (vet[i, 0] == letra)
                {
                    return i;
                }

            }
            throw new Exception("ERROR: Terminal '" + letra + "' não reconhecido.");
        }
        public int posicaoTerminal(String[,] vet, char letra)
        {
            for (int i = 0; i < vet.GetUpperBound(1)+1; i++)
            {
                if (vet[0, i] == letra.ToString())
                {
                    Console.WriteLine(vet[0, i]);
                    return i;
                }

            }
            throw new Exception("ERROR: Terminal '"+letra+"' não reconhecido.");
        }
        public void dividirString(String dev) {
            String[] vet = dev.Split(':');
            if (vet.Length > 1)
            {
                if (vet[1] != "&") {
                    foreach (char e in vet[1].Reverse())
                    {
                        Console.WriteLine("EMPILHANDO -> "+e);
                        pilha.Push(e.ToString());
                    }
                    imprimirPilha();
                }
            }
        }
        public void imprimirPilha() {
            Console.WriteLine("--- PILHA --- ");
            foreach (string a in pilha)
            {
                Console.WriteLine(" ->" + a);
            }
            Console.WriteLine("--------------");
        }
    }
}

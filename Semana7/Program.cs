using System;
using System.Collections.Generic;

namespace Semana7
{
    // --- CLASE 1: Verificador de Paréntesis ---
    public class VerificadorParentesis
    {
        public static bool EstaBalanceada(string expresion)
        {
            Stack<char> pila = new Stack<char>();
            foreach (char c in expresion)
            {
                if (c == '(' || c == '[' || c == '{')
                {
                    pila.Push(c);
                }
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (pila.Count == 0) return false;
                    char apertura = pila.Pop();
                    if (!EsParCorrecto(apertura, c)) return false;
                }
            }
            return pila.Count == 0;
        }

        private static bool EsParCorrecto(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '[' && cierre == ']') ||
                   (apertura == '{' && cierre == '}');
        }
    }

    // --- CLASE 2: Torres de Hanoi ---
    public class HanoiPilas
    {
        public static Stack<int> Origen = new Stack<int>();
        public static Stack<int> Auxiliar = new Stack<int>();
        public static Stack<int> Destino = new Stack<int>();

        public static void ResolverHanoi(int n, Stack<int> fuente, Stack<int> destino, Stack<int> aux, string s, string d, string a)
        {
            if (n > 0)
            {
                ResolverHanoi(n - 1, fuente, aux, destino, s, a, d);
                int disco = fuente.Pop();
                destino.Push(disco);
                Console.WriteLine($"Mover disco {disco} de Torre {s} a Torre {d}");
                ResolverHanoi(n - 1, aux, destino, fuente, a, d, s);
            }
        }
    }

    // --- CLASE PRINCIPAL ---
    class Program
    {
        static void Main(string[] args)
        {
            // Prueba de Paréntesis
            string formula = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
            bool balanceada = VerificadorParentesis.EstaBalanceada(formula);
            Console.WriteLine($"Fórmula: {formula}");
            Console.WriteLine(balanceada ? "✅ Salida: Fórmula balanceada." : "❌ Salida: Fórmula NO balanceada.");

            Console.WriteLine("\n" + new string('-', 40) + "\n");

            // Prueba de Hanoi
            int discos = 3;
            // Limpiamos las pilas por si acaso
            HanoiPilas.Origen.Clear();
            HanoiPilas.Auxiliar.Clear();
            HanoiPilas.Destino.Clear();

            for (int i = discos; i >= 1; i--) HanoiPilas.Origen.Push(i);

            Console.WriteLine($"Iniciando Torres de Hanoi con {discos} discos:");
            HanoiPilas.ResolverHanoi(discos, HanoiPilas.Origen, HanoiPilas.Destino, HanoiPilas.Auxiliar, "A", "C", "B");
            
            Console.WriteLine("\n✅ ¡Torres de Hanoi resueltas!");
            Console.ReadKey(); // Para que no se cierre la consola
        }
    }
}
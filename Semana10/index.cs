using System;
using System.Collections.Generic;
using System.Linq;

namespace CampanaVacunacion
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Generar el Universo de 500 ciudadanos
            HashSet<string> universoCiudadanos = new HashSet<string>();
            for (int i = 1; i <= 500; i++)
            {
                universoCiudadanos.Add($"Ciudadano {i}");
            }

            // 2. Generar conjunto de vacunados con Pfizer (75 ciudadanos)
            HashSet<string> pfizer = new HashSet<string>();
            for (int i = 1; i <= 75; i++)
            {
                pfizer.Add($"Ciudadano {i}");
            }

            // 3. Generar conjunto de vacunados con AstraZeneca (75 ciudadanos)
            // Nota: Para simular el cruce (ambas dosis), empezamos desde el 50
            HashSet<string> astraZeneca = new HashSet<string>();
            for (int i = 50; i <= 124; i++)
            {
                astraZeneca.Add($"Ciudadano {i}");
            }

            // --- OPERACIONES DE TEORÍA DE CONJUNTOS ---

            // A. Ciudadanos que han recibido AMBAS dosis (Intersección)
            HashSet<string> ambasDosis = new HashSet<string>(pfizer);
            ambasDosis.IntersectWith(astraZeneca);

            // B. Ciudadanos que NO se han vacunado (Diferencia: Universo - Unión de ambos)
            HashSet<string> todosVacunados = new HashSet<string>(pfizer);
            todosVacunados.UnionWith(astraZeneca);
            
            HashSet<string> noVacunados = new HashSet<string>(universoCiudadanos);
            noVacunados.ExceptWith(todosVacunados);

            // C. Ciudadanos que SOLO tienen Pfizer (Diferencia: Pfizer - AstraZeneca)
            HashSet<string> soloPfizer = new HashSet<string>(pfizer);
            soloPfizer.ExceptWith(astraZeneca);

            // D. Ciudadanos que SOLO tienen AstraZeneca (Diferencia: AstraZeneca - Pfizer)
            HashSet<string> soloAstraZeneca = new HashSet<string>(astraZeneca);
            soloAstraZeneca.ExceptWith(pfizer);

            // --- MOSTRAR RESULTADOS ---
            MostrarResultados("CIUDADANOS NO VACUNADOS", noVacunados, 5);
            MostrarResultados("CIUDADANOS CON AMBAS DOSIS", ambasDosis);
            MostrarResultados("CIUDADANOS SOLO CON PFIZER", soloPfizer);
            MostrarResultados("CIUDADANOS SOLO CON ASTRAZENECA", soloAstraZeneca);
        }

        static void MostrarResultados(string titulo, HashSet<string> conjunto, int limite = 10)
        {
            Console.WriteLine($"\n--- {titulo} ({conjunto.Count} registros) ---");
            // Mostramos solo los primeros para no saturar la consola
            foreach (var item in conjunto.Take(limite))
            {
                Console.WriteLine($"- {item}");
            }
            if (conjunto.Count > limite) Console.WriteLine("  ... entre otros.");
        }
    }
}
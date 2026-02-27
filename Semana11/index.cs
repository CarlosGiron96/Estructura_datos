using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TraductorDiccionario
{
    class Program
    {
        // Ruta del archivo de texto
        static string rutaArchivo = "diccionario.txt";
        
        // Diccionario principal
        static Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            CargarDiccionario();

            int opcion;
            do
            {
                Console.WriteLine("\n==================== MENÚ ====================");
                Console.WriteLine("1. Traducir una frase (Español -> Inglés)");
                Console.WriteLine("2. Agregar palabras al diccionario");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1: TraducirFrase(); break;
                    case 2: AgregarPalabra(); break;
                    case 0: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            } while (opcion != 0);
        }

        static void CargarDiccionario()
        {
            // Si el archivo no existe, creamos uno con las 10 palabras iniciales
            if (!File.Exists(rutaArchivo))
            {
                var iniciales = new Dictionary<string, string>
                {
                    {"Time", "Tiempo"}, {"Person", "Persona"}, {"Year", "Año"},
                    {"Way", "Camino"}, {"Day", "Día"}, {"Thing", "Cosa"},
                    {"Man", "Hombre"}, {"World", "Mundo"}, {"Life", "Vida"}, {"Hand", "Mano"}
                };
                GuardarTodoElDiccionario(iniciales);
            }

            // Leer del archivo
            foreach (var linea in File.ReadAllLines(rutaArchivo))
            {
                var partes = linea.Split('|');
                if (partes.Length == 2)
                    diccionario[partes[0]] = partes[1];
            }
        }

        static void TraducirFrase()
        {
            Console.WriteLine("\nIngrese la frase en ESPAÑOL:");
            string entrada = Console.ReadLine();
            string[] palabras = entrada.Split(' ');
            List<string> fraseTraducida = new List<string>();

            foreach (string palabra in palabras)
            {
                string limpia = palabra.Trim(new char[] { '.', ',', '!', '?' });
                
                // Buscamos el VALOR (español) para obtener la LLAVE (inglés)
                var entradaDic = diccionario.FirstOrDefault(x => x.Value.Equals(limpia, StringComparison.OrdinalIgnoreCase));

                if (entradaDic.Key != null)
                {
                    fraseTraducida.Add(palabra.ToLower().Replace(limpia.ToLower(), entradaDic.Key.ToLower()));
                }
                else
                {
                    fraseTraducida.Add(palabra);
                }
            }

            Console.WriteLine("\nTraducción (Español a Inglés):");
            Console.WriteLine(string.Join(" ", fraseTraducida));
        }

        static void AgregarPalabra()
        {
            Console.Write("\nIngrese la palabra en Inglés: ");
            string ingles = Console.ReadLine().Trim();
            Console.Write("Ingrese su traducción al Español: ");
            string espanol = Console.ReadLine().Trim();

            if (!diccionario.ContainsKey(ingles))
            {
                diccionario.Add(ingles, espanol);
                // Guardar en el archivo (append)
                File.AppendAllLines(rutaArchivo, new[] { $"{ingles}|{espanol}" });
                Console.WriteLine("¡Palabra guardada en el archivo!");
            }
            else
            {
                Console.WriteLine("Esa palabra ya existe.");
            }
        }

        static void GuardarTodoElDiccionario(Dictionary<string, string> datos)
        {
            List<string> lineas = datos.Select(x => $"{x.Key}|{x.Value}").ToList();
            File.WriteAllLines(rutaArchivo, lineas);
        }
    }
}
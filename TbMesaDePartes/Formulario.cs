using System;

namespace MesaDePartes
{
    // Formulario donde el ciudadano ingresa sus datos (DNI, nombre, asunto) y se validan
    public class Formulario
    {
        // Escribe un titulo entre lineas
        public static void Titulo(string texto)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("  " + texto);
            Console.WriteLine("==========================================");
        }

        // El DNI debe tener exactamente 8 numeros
        public static string LeerDni()
        {
            while (true)
            {
                Console.Write("DNI (8 digitos): ");
                string dni = (Console.ReadLine() ?? "").Trim();

                bool valido = dni.Length == 8;
                for (int i = 0; i < dni.Length && valido; i++)
                {
                    if (!char.IsDigit(dni[i])) valido = false;
                }

                if (valido) return dni;
                Console.WriteLine("  DNI invalido: deben ser exactamente 8 numeros.");
            }
        }

        // Un texto que no puede quedar vacio
        public static string LeerTextoObligatorio(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string texto = (Console.ReadLine() ?? "").Trim();
                if (texto.Length > 0) return texto;
                Console.WriteLine("  Este campo no puede estar vacio.");
            }
        }

        // El asunto no puede estar vacio ni pasar de 100 caracteres
        public static string LeerAsunto()
        {
            while (true)
            {
                Console.Write("Asunto (maximo 100 caracteres): ");
                string asunto = (Console.ReadLine() ?? "").Trim();

                if (asunto.Length == 0)
                    Console.WriteLine("  El asunto no puede estar vacio.");
                else if (asunto.Length > 100)
                    Console.WriteLine($"  El asunto tiene {asunto.Length} caracteres; el maximo es 100.");
                else
                    return asunto;
            }
        }

        // Un numero entero entre un minimo y un maximo
        public static int LeerEntero(string mensaje, int min, int max)
        {
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out int valor) && valor >= min && valor <= max)
                    return valor;
                Console.WriteLine($"  Ingrese un numero entre {min} y {max}.");
            }
        }
    }
}
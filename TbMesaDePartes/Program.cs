using System;
using System.Text;

namespace MesaDePartes
{
    // Solo el menu principal: llama a los metodos de las otras clases
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            GestorTramites.CargarDatosDePrueba();

            Formulario.Titulo("MESA DE PARTES DIGITAL");
            Console.WriteLine("Identifiquese para continuar.\n");
            string dniUsuario = Formulario.LeerDni();
            string nombreUsuario = Formulario.LeerTextoObligatorio("Nombres y apellidos: ");
            GestorTramites.ActualizarNombre(dniUsuario, nombreUsuario);

            int opcion;
            do
            {
                MostrarMenu(dniUsuario, nombreUsuario);
                opcion = Formulario.LeerEntero("Seleccione una opcion: ", 0, 5);
                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        GestorTramites.RegistrarTramite(opcion - 1, dniUsuario, nombreUsuario);
                        break;
                    case 5:
                        GestorTramites.EstatusDeTramite(dniUsuario);
                        break;
                    case 0:
                        Console.WriteLine("Gracias por usar la Mesa de Partes Digital.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione una tecla para volver al menu...");
                    Console.ReadKey(true);
                }
            } while (opcion != 0);
        }

        static void MostrarMenu(string dni, string nombre)
        {
            Console.Clear();
            Formulario.Titulo("MESA DE PARTES");
            Console.WriteLine($"Usuario: {nombre} (DNI {dni})\n");
            for (int i = 0; i < GestorTramites.tramites.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {GestorTramites.tramites[i]}");
            }
            Console.WriteLine("  5. Estatus de tramite");
            Console.WriteLine("  0. Salir\n");
        }
    }
}
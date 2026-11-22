using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace MesaDePartes
{
    // Logica del sistema: registrar, consultar, ordenar, buscar y guardar
    public class GestorTramites
    {
        // ---------------------------------------------------------------
        // DATOS
        // ---------------------------------------------------------------

        // Tramites y su area encargada (la posicion 0 de tramites va con la 0 de areas)
        public static string[] tramites =
        {
            "Licencia de funcionamiento",
            "Certificado de seguridad en defensa civil",
            "Acta de matrimonio",
            "Consulta de deuda tributaria"
        };

        public static string[] areas =
        {
            "Gerencia de Desarrollo Economico",
            "Oficina de Defensa Civil",
            "Oficina de Registro Civil",
            "Gerencia de Administracion Tributaria"
        };

        static List<Expediente> expedientes = new List<Expediente>();
        static int contador = 0;
        const string ARCHIVO = "expedientes.txt";

        // ---------------------------------------------------------------
        // AGREGAR EXPEDIENTES
        // ---------------------------------------------------------------
        public static void CargarDatosDePrueba()
        {
            // El nombre queda vacio: se completa con el que escriba la persona al ingresar
            AgregarExpediente("22222222", "", 3,
                "Solicito la consulta de mi deuda tributaria desde el año pasado hasta la fecha.",
                Estatus.Aprobado);

            AgregarExpediente("88888888", "", 0,
                "Solicito la licencia de funcionamiento para mi local comercial, con la documentacion completa.",
                Estatus.EnProceso);

            GuardarEnArchivo();
        }

        public static Expediente AgregarExpediente(string dni, string nombre, int indiceTramite, string asunto, Estatus estatus)
        {
            contador++;
            Expediente exp = new Expediente
            {
                NumeroOperacion = $"OP-{DateTime.Now.Year}-{contador:0000}",
                Dni = dni,
                NombreCompleto = nombre,
                Tramite = tramites[indiceTramite],
                Area = areas[indiceTramite],
                Asunto = asunto,
                Fecha = DateTime.Now,
                Estatus = estatus
            };
            expedientes.Add(exp);
            return exp;
        }

        // Los expedientes de este DNI muestran el nombre que se escribio
        public static void ActualizarNombre(string dni, string nombre)
        {
            for (int i = 0; i < expedientes.Count; i++)
            {
                if (expedientes[i].Dni == dni)
                    expedientes[i].NombreCompleto = nombre;
            }
            GuardarEnArchivo();
        }

        // ---------------------------------------------------------------
        // OPCIONES DEL MENU
        // ---------------------------------------------------------------
        public static void RegistrarTramite(int indice, string dni, string nombre)
        {
            Formulario.Titulo(tramites[indice].ToUpper());
            Console.WriteLine($"Area encargada: {areas[indice]}\n");

            Console.WriteLine($"DNI: {dni}");
            Console.WriteLine($"Nombre y apellido: {nombre}");

            // El numero de operacion lo genera el sistema
            Console.WriteLine($"Numero de operacion: OP-{DateTime.Now.Year}-{contador + 1:0000}\n");

            string asunto = Formulario.LeerAsunto();

            Expediente exp = AgregarExpediente(dni, nombre, indice, asunto, Estatus.EnProceso);
            GuardarEnArchivo();

            Console.WriteLine("\nTramite registrado correctamente.");
            exp.Mostrar();
        }

        public static void EstatusDeTramite(string dni)
        {
            Formulario.Titulo("ESTATUS DE TRAMITE");

            // Expedientes del usuario
            List<Expediente> mios = new List<Expediente>();
            for (int i = 0; i < expedientes.Count; i++)
            {
                if (expedientes[i].Dni == dni)
                    mios.Add(expedientes[i]);
            }

            if (mios.Count == 0)
            {
                Console.WriteLine("Usted aun no tiene tramites registrados.");
                return;
            }

            // Ordenados por numero de operacion 
            Expediente[] vector = mios.ToArray();
            OrdenarPorNumero(vector);

            for (int i = 0; i < vector.Length; i++)
            {
                vector[i].Mostrar();
            }

            // Actualizar estatus (simula lo que haria el area encargada)
            Console.WriteLine("\nPara actualizar el estatus escriba el numero de operacion.");
            Console.Write("(Enter para volver al menu): ");
            string numero = (Console.ReadLine() ?? "").Trim();
            if (numero.Length == 0) return;

            Expediente exp = BuscarPorNumero(vector, numero);
            if (exp == null)
            {
                Console.WriteLine("No se encontro ese numero de operacion entre sus tramites.");
                return;
            }

            Console.WriteLine($"\nEstatus actual: {exp.TextoEstatus()}");
            Console.WriteLine("  1. En proceso");
            Console.WriteLine("  2. En observacion");
            Console.WriteLine("  3. Aprobado");
            Console.WriteLine("  4. Finalizado");
            int nuevo = Formulario.LeerEntero("Nuevo estatus: ", 1, 4);

            exp.Estatus = (Estatus)(nuevo - 1);
            GuardarEnArchivo();
            Console.WriteLine("\nEstatus actualizado.");
            exp.Mostrar();
        }

        // ---------------------------------------------------------------
        // ORDENAR Y BUSCAR
        // ---------------------------------------------------------------

        // Ordenamiento por seleccion directa
        public static void OrdenarPorNumero(Expediente[] v)
        {
            for (int i = 0; i < v.Length - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < v.Length; j++)
                {
                    if (string.Compare(v[j].NumeroOperacion, v[menor].NumeroOperacion, StringComparison.Ordinal) < 0)
                        menor = j;
                }
                if (menor != i)
                {
                    Expediente aux = v[i];
                    v[i] = v[menor];
                    v[menor] = aux;
                }
            }
        }

        // Busqueda lineal
        public static Expediente BuscarPorNumero(Expediente[] v, string numero)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (string.Equals(v[i].NumeroOperacion, numero, StringComparison.OrdinalIgnoreCase))
                    return v[i];
            }
            return null;
        }

        // ---------------------------------------------------------------
        // ARCHIVO
        // ---------------------------------------------------------------
        public static void GuardarEnArchivo()
        {
            List<string> lineas = new List<string>();
            for (int i = 0; i < expedientes.Count; i++)
            {
                Expediente e = expedientes[i];
                lineas.Add($"{e.NumeroOperacion}|{e.Dni}|{e.NombreCompleto}|{e.Tramite}|{e.Area}|{e.Asunto}|{e.Fecha:yyyy-MM-dd}|{e.TextoEstatus()}");
            }
            File.WriteAllLines(ARCHIVO, lineas, Encoding.UTF8);
        }
    }
}
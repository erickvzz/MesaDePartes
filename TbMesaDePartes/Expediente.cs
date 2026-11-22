using System;

namespace MesaDePartes
{
    public enum Estatus
    {
        EnProceso,
        EnObservacion,
        Aprobado,
        Finalizado
    }

    // Representa UN tramite registrado
    public class Expediente
    {
        public string NumeroOperacion { get; set; }
        public string Dni { get; set; }
        public string NombreCompleto { get; set; }
        public string Tramite { get; set; }
        public string Area { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public Estatus Estatus { get; set; }

        public string TextoEstatus()
        {
            switch (Estatus)
            {
                case Estatus.EnProceso: return "En proceso";
                case Estatus.EnObservacion: return "En observacion";
                case Estatus.Aprobado: return "Aprobado";
                default: return "Finalizado";
            }
        }

        public void Mostrar()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"N° operacion : {NumeroOperacion}");
            Console.WriteLine($"Tramite      : {Tramite}");
            Console.WriteLine($"Area         : {Area}");
            Console.WriteLine($"Solicitante  : {NombreCompleto} (DNI {Dni})");
            Console.WriteLine($"Asunto       : {Asunto}");
            Console.WriteLine($"Fecha        : {Fecha:dd/MM/yyyy}");
            Console.WriteLine($"Estatus      : {TextoEstatus()}");
            Console.WriteLine("------------------------------------------");
        }
    }
}
using System;
using sistema_bancario.Cliente;
using sistema_bancario.Enums;

namespace sistema_bancario.Credito
{
    public abstract class SolicitudCredito
    {
        public int id_solicitud_credito { get; set; }
        public PerfilCliente cliente { get; set; }
        public double monto_solicitado { get; set; }
        public int plazo_meses { get; set; }
        public EstadoCredito estado { get; set; }
        public TipoCredito tipo { get; set; }
        public Propiedad propiedad { get; set; }

        public SolicitudCredito(int id_solicitud_credito, PerfilCliente cliente, double monto_solicitado, int plazo_meses, TipoCredito tipo)
        {
            this.id_solicitud_credito = id_solicitud_credito;
            this.cliente = cliente;
            this.monto_solicitado = monto_solicitado;
            this.plazo_meses = plazo_meses;
            this.tipo = tipo;
            this.estado = EstadoCredito.PENDIENTE;
        }
        
        public SolicitudCredito(int id_solicitud_credito, PerfilCliente cliente, double monto_solicitado, int plazo_meses, TipoCredito tipo, Propiedad propiedad)
        {
            this.id_solicitud_credito = id_solicitud_credito;
            this.cliente = cliente;
            this.monto_solicitado = monto_solicitado;
            this.plazo_meses = plazo_meses;
            this.tipo = tipo;
            this.estado = EstadoCredito.PENDIENTE;
            this.propiedad = propiedad;
        }

        public double CalcularCuotaMensual()
        {
            return monto_solicitado / plazo_meses;
        }

        public abstract bool EsAceptable();

        public virtual void MostrarDetalle()
        {
            Console.WriteLine(string.Format("Solicitud #{0}", id_solicitud_credito));
            Console.WriteLine(string.Format("Cliente: {0} {1}", cliente.Nombre, cliente.Apellido));
            Console.WriteLine(string.Format("Monto: ${0}", monto_solicitado));
            Console.WriteLine(string.Format("Plazo: {0} meses", plazo_meses));
            Console.WriteLine(string.Format("Cuota mensual: ${0}", Math.Round(CalcularCuotaMensual(), 2)));
            Console.WriteLine(string.Format("Tipo: {0}", tipo));
            Console.WriteLine(string.Format("Estado: {0}", estado));
        }
    }
}

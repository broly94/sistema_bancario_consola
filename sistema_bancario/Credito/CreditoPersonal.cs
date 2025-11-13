using System;
using sistema_bancario.Cliente;
using sistema_bancario.Enums;

namespace sistema_bancario.Credito
{
    public class CreditoPersonal : SolicitudCredito
    {
        public CreditoPersonal(int id_solicitud_credito, PerfilCliente cliente, double monto_solicitado, int plazo_meses)
            : base(id_solicitud_credito, cliente, monto_solicitado, plazo_meses, TipoCredito.PERSONAL)
        {
        }

        public override bool EsAceptable()
        {
            double sueldoAnual = cliente.SueldoNeto * 12;
            double cuotaMensual = CalcularCuotaMensual();

            return sueldoAnual >= 750000 && cuotaMensual <= cliente.SueldoNeto * 0.7;
        }

//        public double CalcularCuotaMensual()
//        {
//            return monto_solicitado / plazo_meses;
//        }
    }
}

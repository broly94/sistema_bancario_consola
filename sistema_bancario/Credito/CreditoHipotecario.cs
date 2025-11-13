using System;
using sistema_bancario.Cliente;
using sistema_bancario.Enums;

namespace sistema_bancario.Credito
{
    public class CreditoHipotecario : SolicitudCredito
    {
        public double valor_fiscal_propiedad { get; set; }

        public CreditoHipotecario(int id_solicitud_credito, PerfilCliente cliente, double monto_solicitado, int plazo_meses, Propiedad propiedad)
            : base(id_solicitud_credito, cliente, monto_solicitado, plazo_meses, TipoCredito.HIPOTECARIO, propiedad)
        {
            valor_fiscal_propiedad = propiedad.ValorFiscal;
        }

        public override bool EsAceptable()
        {
            double sueldoAnual = cliente.SueldoNeto * 12;
            double cuotaMensual = CalcularCuotaMensual();

            return (cliente.Edad <= 65 && cliente.Edad >= 18) && monto_solicitado <= valor_fiscal_propiedad * 0.7 && cuotaMensual <= cliente.SueldoNeto * 0.5;
        }

//        public double CalcularCuotaMensual()
//        {
//            return monto_solicitado / plazo_meses;
//        }
    }
}


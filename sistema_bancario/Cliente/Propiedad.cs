/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 09/10/2025
 * Hora: 7:27
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace sistema_bancario.Cliente
{

	public class Propiedad
	{
		private int id_propiedad;
		private string descripcion;
		private string direccion;
		private double valor_fiscal;
		private PerfilCliente propietario;
		
		public Propiedad(int id_propiedad, string descripcion, string direccion, double valor_fiscal, PerfilCliente propietario)
		{
			this.id_propiedad = id_propiedad;
			this.descripcion = descripcion;
			this.direccion = direccion;
			this.valor_fiscal = valor_fiscal;
			this.propietario = propietario;
		}
		
		public int IdPropiedad
		{
			get { return id_propiedad; } set { id_propiedad = value; }
		}
		
		public string Descripcion
		{
			get { return descripcion; }
		}
		
		public string Direccion
		{
			get { return direccion; }
		}
		
		public double ValorFiscal
		{
			get { return valor_fiscal; }
		}
		
		public PerfilCliente Propietario
		{
			get { return propietario; }
		}
	}
}

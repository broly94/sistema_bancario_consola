/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 09/10/2025
 * Hora: 7:27
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace sistema_bancario.Cliente
{
	public class PerfilCliente
	{
		private int id_cliente;
		private string nombre;
		private string apellido;
		private string direccion;
		private int edad;
		private double sueldo_neto;
		private Propiedad propiedad;
		
		public PerfilCliente(int id_cliente, string nombre, string apellido, string direccion, int edad, double sueldo_neto)
		{
			this.id_cliente = id_cliente;
			this.nombre = nombre;
			this.apellido = apellido;
			this.direccion = direccion;
			this.edad = edad;
			this.sueldo_neto = sueldo_neto;
		}
		
		public int IdCliente
		{
			get { return id_cliente; } set { id_cliente = value; }
		}
		
		public string Nombre
		{
			get { return nombre; }
		}
		public string Apellido
		{
			get { return apellido; }
		}
		public string Direccion
		{
			get { return direccion; }
		}
		public int Edad
		{
			get { return edad; }
		}
		public double SueldoNeto
		{
			get { return sueldo_neto; }
		}
		public Propiedad Propiedad
		{
			get { return propiedad; } set { propiedad = value; }
		}
				
	}
}

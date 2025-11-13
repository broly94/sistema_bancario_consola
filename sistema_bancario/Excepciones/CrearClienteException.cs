/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 09/10/2025
 * Hora: 7:35
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Runtime.Serialization;

namespace sistema_bancario.Excepciones
{
	/// <summary>
	/// Desctiption of CrearClienteException.
	/// </summary>
	public class CrearClienteException : Exception, ISerializable
	{
		public CrearClienteException()
		{
		}

	 	public CrearClienteException(string message) : base(message)
		{
		}

		public CrearClienteException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected CrearClienteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
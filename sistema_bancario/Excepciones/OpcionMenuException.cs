/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 12/10/2025
 * Hora: 17:06
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Runtime.Serialization;

namespace sistema_bancario.Excepciones
{
	/// <summary>
	/// Desctiption of OpcionMenuException.
	/// </summary>
	public class OpcionMenuException : Exception, ISerializable
	{
		public int input { get; set; }
		
		public OpcionMenuException()
		{
		}

		public OpcionMenuException(string message, int input) : base(message)
		{
			this.input = input;
		}

		public OpcionMenuException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected OpcionMenuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
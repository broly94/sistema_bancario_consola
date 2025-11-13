/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 09/10/2025
 * Hora: 7:34
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Runtime.Serialization;

namespace sistema_bancario.Excepciones
{
	/// <summary>
	/// Desctiption of SolicitudRechazadaException.
	/// </summary>
	public class SolicitudRechazadaException : Exception, ISerializable
	{
		public SolicitudRechazadaException()
		{
		}

	 	public SolicitudRechazadaException(string message) : base(message)
		{
		}

		public SolicitudRechazadaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected SolicitudRechazadaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
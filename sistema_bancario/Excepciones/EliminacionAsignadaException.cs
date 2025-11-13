/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 09/10/2025
 * Hora: 7:36
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Runtime.Serialization;

namespace sistema_bancario.Excepciones
{
	/// <summary>
	/// Desctiption of EliminacionAsignadaException.
	/// </summary>
	public class EliminacionAsignadaException : Exception, ISerializable
	{
		public EliminacionAsignadaException()
		{
		}

	 	public EliminacionAsignadaException(string message) : base(message)
		{
		}

		public EliminacionAsignadaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected EliminacionAsignadaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
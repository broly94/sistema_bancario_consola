using System;
using System.Collections.Generic;
using sistema_bancario.Credito;
using sistema_bancario.Cliente;
using sistema_bancario.Enums;
using sistema_bancario.Excepciones;
using System.Linq;

namespace sistema_bancario.Banca
{
	public class Banco
	{
		private List<PerfilCliente> clientes = new List<PerfilCliente>();
		private List<SolicitudCredito> solicitud_credito = new List<SolicitudCredito>();
		private List<Propiedad> propiedades = new List<Propiedad>();
		
		/* CLIENTES */
		
		public void CrearCliente(string nombre, string apellido, string direccion, int edad, double sueldo_neto)
		{
			
			int id_cliente = 0;
			
			if(clientes.Count == 0)
			{
				id_cliente = 1;
			} else {
				id_cliente = clientes.LastOrDefault().IdCliente + 1;
			}
			
			if (clientes.Any(c => c.IdCliente == id_cliente))
				throw new CrearClienteException("El cliente ya existe.");

			PerfilCliente cliente = new PerfilCliente(id_cliente, nombre, apellido, direccion, edad, sueldo_neto);
			
			clientes.Add(cliente);
			
			Console.Clear();
			
			Console.WriteLine("Cliente registrado.");
			
		}
		
		public void EliminarCliente(int id_cliente)
		{
			PerfilCliente cliente = ObtenerCliente(id_cliente);
			if(solicitud_credito.Any(credito => credito.cliente.IdCliente == id_cliente))
				throw new EliminacionAsignadaException("Error, el cliente tiene credito activo");
			
			clientes.Remove(cliente);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Cliente eliminado");
			Console.ResetColor();
		}
		
		public PerfilCliente ObtenerCliente(int idCliente)
		{
			if(!clientes.Any(c => c.IdCliente == idCliente)) {
				throw new CrearClienteException("El cliente no existe");
			}
			PerfilCliente cliente = clientes.First(c => c.IdCliente == idCliente);
			return cliente;
		}
		
		public void ListarClientes()
		{
			if(clientes.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No hay clientes registrados");
				Console.ResetColor();
				return;
			}
			
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n===Lista de Clientes===");
			Console.ResetColor();
			
			foreach(PerfilCliente cliente in clientes)
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.WriteLine("Id: " +  cliente.IdCliente);
				Console.WriteLine("Nombre: " +  cliente.Nombre);
				Console.WriteLine("Apellido: " + cliente.Apellido);
				Console.WriteLine("Direccion: " + cliente.Direccion);
				Console.WriteLine("Edad: " + cliente.Edad);
				Console.WriteLine("Sueldo Neto: " + cliente.SueldoNeto);
				Console.WriteLine("Sueldo Anual: " + cliente.SueldoNeto * 12);
				Console.ResetColor();
				
				if(cliente.Propiedad != null)
				{
					Console.WriteLine("=== Propiedad Registrada ===");
					Console.WriteLine("Descripción: " + cliente.Propiedad.Descripcion);
					Console.WriteLine("Dirección: " + cliente.Propiedad.Direccion);
					Console.WriteLine("Valor Fiscal: " + cliente.Propiedad.ValorFiscal);
					Console.WriteLine("============================");
				}
				Console.WriteLine("==========");
			}
		}
		
		public void CrearPropiedadCliente(string descripcion, string direccion, double valor_fiscal, PerfilCliente propietario)
		{
			int id_propiedad = 0;
			
			if(propiedades.Count == 0)
			{
				id_propiedad = 1;
			} else {
				id_propiedad = propiedades.LastOrDefault().IdPropiedad + 1;
			}
			
			Propiedad propiedad = new Propiedad(id_propiedad, descripcion, direccion, valor_fiscal, propietario);
			
			propiedades.Add(propiedad);
			
			PerfilCliente cliente =  ObtenerCliente(propietario.IdCliente);
			
			cliente.Propiedad = propiedad;
			
			Console.Clear();
			
			Console.WriteLine("Propiedad agregada al cliente: " + cliente.Nombre + " " + cliente.Apellido);
		}
		
		/* CREDITOS */
		
		public void CrearSolicitudCredito(TipoCredito tipo, int id_cliente, double monto_solicitado, int plazo_meses)
		{
			int id_credito = 0;
			
			if(solicitud_credito.Count == 0)
			{
				id_credito = 1;
			} else {
				id_credito = solicitud_credito.LastOrDefault().id_solicitud_credito + 1;
			}
			
			PerfilCliente cliente = ObtenerCliente(id_cliente);
			
			if(tipo == TipoCredito.PERSONAL){
				CreditoPersonal cp = new CreditoPersonal(id_credito, cliente, monto_solicitado, plazo_meses);
				solicitud_credito.Add(cp);
				cp.MostrarDetalle();
			}
			
			if(tipo == TipoCredito.HIPOTECARIO)
			{
				if(cliente.Propiedad != null)
				{
					CreditoHipotecario ch = new CreditoHipotecario(id_credito, cliente, monto_solicitado, plazo_meses, cliente.Propiedad);
					Console.WriteLine(ch);
					solicitud_credito.Add(ch);
					ch.MostrarDetalle();
				} else{
					throw new Exception("Error, el cliente no tiene propiedad registrada");
				}
			}
		}
		
		public void EvaluarSolicitudesPendientes()
		{
			foreach (SolicitudCredito solicitud in solicitud_credito)
			{
				if (solicitud.estado == EstadoCredito.PENDIENTE)
				{
					if (solicitud.EsAceptable())
					{
						solicitud.estado = EstadoCredito.ACEPTADO;
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Solicitud #{0} aceptada.", solicitud.id_solicitud_credito);
					}
					else
					{
						solicitud.estado = EstadoCredito.RECHAZADO;
						throw new SolicitudRechazadaException("Solicitudes rechazadas");
					}
					Console.ResetColor();
				}
			}
		}
		
		public void EliminarSolicitudCredito(int id_solicitud_credito)
		{
			SolicitudCredito solicitud = solicitud_credito.Find(s => s.id_solicitud_credito == id_solicitud_credito);

			if (solicitud == null)
			{
				throw new Exception("No se encontró una solicitud con ese ID.");
			}

			if (solicitud.estado == EstadoCredito.ACEPTADO)
			{
				throw new EliminacionAsignadaException("No se puede eliminar una solicitud que ya fue aceptada.");
			}

			solicitud_credito.Remove(solicitud);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Solicitud eliminada correctamente.");
			Console.ResetColor();
		}

		
		
		public double MontoTotalCreditosAprobados()
		{
			double total = 0;

			foreach (SolicitudCredito solicitud in solicitud_credito)
			{
				if (solicitud.estado == EstadoCredito.ACEPTADO)
				{
					total += solicitud.monto_solicitado;
				}
			}

			return total;
		}
		
		public Tuple<int, int> CantidadCreditosPorTipo()
		{
			int contador_credito_personal = 0;
			int contador_credito_hipotecario = 0;
			
			foreach (SolicitudCredito solicitud in solicitud_credito)
			{
				if (solicitud.tipo == TipoCredito.PERSONAL)
				{
					contador_credito_personal++;
				} else {
					contador_credito_hipotecario++;
				}
			}

			return new Tuple<int, int>(contador_credito_personal, contador_credito_hipotecario);
		}

		public double PromedioEdadClientesActivos()
		{
			int sumaEdad = 0;
			int cantidad = 0;

			foreach (SolicitudCredito solicitud in solicitud_credito)
			{
				if (solicitud.estado == EstadoCredito.ACEPTADO)
				{
					sumaEdad += solicitud.cliente.Edad;
					cantidad++;
				}
			}

			if (cantidad == 0)
				return 0;

			return (double)sumaEdad / cantidad;
		}
		
		public bool ValidarCreditos()
		{
			return false;
		}
		
		public void ListarDetallesCreditos()
		{
			
			if(solicitud_credito.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No hay solicitudes para mostrar");
				Console.ResetColor();
			}
			
			foreach(SolicitudCredito solicitudes in solicitud_credito)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				solicitudes.MostrarDetalle();
				Console.WriteLine('\n');
				Console.ResetColor();
			}
		}
		
		public void ListarSolicitudesPersonalesAprobadas()
		{
			if(solicitud_credito.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No hay solicitudes para mostrar");
				Console.ResetColor();
			}
			
			foreach(SolicitudCredito solicitudes in solicitud_credito)
			{
				if(solicitudes.estado == EstadoCredito.ACEPTADO && solicitudes.tipo == TipoCredito.PERSONAL)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					solicitudes.MostrarDetalle();
					Console.WriteLine('\n');
					Console.ResetColor();
				}
			}
		}
		
		public void ListarSolicitudesHipotecarioAprobadas()
		{
			if(solicitud_credito.Count == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("No hay solicitudes para mostrar");
				Console.ResetColor();
			}
			
			foreach(SolicitudCredito solicitudes in solicitud_credito)
			{
				if(solicitudes.estado == EstadoCredito.ACEPTADO && solicitudes.tipo == TipoCredito.HIPOTECARIO)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					solicitudes.MostrarDetalle();
					Console.WriteLine('\n');
					Console.ResetColor();
				}
			}
		}
	}
}

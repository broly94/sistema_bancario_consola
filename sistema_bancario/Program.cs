using System;
using sistema_bancario.Banca;
using sistema_bancario.Enums;
using sistema_bancario.Excepciones;
using sistema_bancario.Cliente;
using sistema_bancario.Helpers;

namespace sistema_bancario
{
	class Program
	{
		public static void Main(string[] args)
		{
			Menu();
			
			Console.ReadKey();
		}
		
		
		
		public static void Menu()
		{
			
			Banco banco = new Banco();
			
			int opcion = 0;
			
			while(opcion != 9)
			{
				ListarMenu();
				
				string entrada = Console.ReadLine();
				
				if(!int.TryParse(entrada, out opcion))
				{
					Console.WriteLine("Debe ingresar un numero. Elija alguna de las opciones del menú.");
					continue;
				}
				
				switch(opcion)
				{
					case 1:
						Console.Clear();
						Console.WriteLine("===Registrar Cliente===\n");
						CrearCliente(banco);
						break;
						
					case 2:
						Console.Clear();
						Console.WriteLine("===Nueva Solicitud de Crédito===\n");
						CrearSolicitudCredito(banco);
						break;
						
					case 3:
						Console.Clear();
						Console.WriteLine("===Crear Propiedad del Cliente===");
						AsignarPropiedadCliente(banco);
						break;
						
					case 4:
						Console.Clear();
						Console.WriteLine("=== Evaluación de Solicitudes Pendientes ===\n");
						try{
							banco.EvaluarSolicitudesPendientes();
						} catch(Exception ex) {
							Console.WriteLine(ex.Message);
						}
						break;
					case 5:
						Console.Clear();
						Console.WriteLine("=== Eliminar Solicitud de Crédito ===\n");
						EliminarSolicitud(banco);
						break;
					case 6:
						Console.Clear();
						Console.WriteLine("=== Eliminar Cliente ===\n");
						EliminarCliente(banco);
						break;
					case 7:
						Console.Clear();
						Console.WriteLine("=== Estadisticas ===\n");
						Estadisticas(banco);
						break;
					case 8:
						Console.Clear();
						Console.WriteLine("=== Impresiones y reportes ===\n");
						ImpresionesYReportes(banco);
						break;
					case 9:
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("Saliendo del sistema... ¡Gracias por usar el sistema bancario!");
						Console.ResetColor();
						break;

				}
			}
			
		}
		
		
		/* CLIENTES */
		
		public static void CrearCliente(Banco banco)
		{
			
			Console.WriteLine("Nombre: ");
			string nombre = Console.ReadLine();
			
			Console.WriteLine("Apellido: ");
			string apellido = Console.ReadLine();
			
			Console.WriteLine("Dirección: ");
			string direccion = Console.ReadLine();
			
			int edad = Validacion.ValidarEdad(); //modifique
			
			double sueldo_neto = Validacion.ValidarDouble("Sueldo Neto: ", "Sueldo Neto");
			
			bool confirmacion = Validacion.ValidarFinalizacion("¿Desea confirmar la creación del cliente? (S) = Si | (Cualquier tecla): No");
			
			if(!confirmacion) return;
			
			try {
				banco.CrearCliente(nombre,apellido,direccion,edad,sueldo_neto);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
		
		public static void EliminarCliente(Banco banco)
		{
			int idCliente = (int)Validacion.ValidarEntero("Ingresa el Id del cliente: ", "Id");
			try {
				banco.EliminarCliente(idCliente);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
		
		/* PROPIEDAD CLIENTE */

		public static void AsignarPropiedadCliente(Banco banco)
		{

			int id_cliente = 0;

			while(true)
			{
				id_cliente = (int)Validacion.ValidarEntero("Id Cliente: ", "Id");

				try {
					banco.ObtenerCliente(id_cliente);
					break;
				} catch (Exception ex) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(ex.Message);
					Console.ResetColor();
					return;
				}
			}

			Console.WriteLine("Descripcion: ");
			string descripcion = Console.ReadLine();

			Console.WriteLine("Dirección: ");
			string direccion = Console.ReadLine();

			double valor_fiscal = Validacion.ValidarDouble("Valor Fiscal: ", "Valor Fiscal");

			bool confirmacion = Validacion.ValidarFinalizacion("¿Desea confirmar la asignación de la propiedad del cliente? (S) = Si | (Cualquier tecla): No");

			if(!confirmacion) return;

			try {
				PerfilCliente cliente = banco.ObtenerCliente(id_cliente);
				banco.CrearPropiedadCliente(descripcion, direccion, valor_fiscal, cliente);
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
		}
		
		/* CREDITOS */
		
		public static void CrearSolicitudCredito(Banco banco)
		{
			
			Console.Clear();
			
			while(true)
			{
				Console.WriteLine("¿Qué tipo de credito quiere crear?");
				Console.WriteLine("Crédito Personal [P] - Crédito Hipotecario [H]");
				Console.WriteLine("Indique una opción: ");
				string entradaTipo = Console.ReadLine().ToLower();
				TipoCredito tipo = TipoCredito.PERSONAL;
				
				if(entradaTipo == "p" || entradaTipo == "h")
				{
					
					switch(entradaTipo)
					{
						case "p":
							tipo = TipoCredito.PERSONAL;
							break;
						case "h":
							tipo = TipoCredito.HIPOTECARIO;
							break;
					}
					
					int id_cliente = Validacion.ValidarEntero("Ingrese el ID del cliente: ", "ID Cliente");
										
					double monto_solicitado = Validacion.ValidarDouble("Ingrese el monto a solicitar: ", "Monto Solicitado");
					
					int plazo_meses = Validacion.ValidarEntero("Ingres el plazo en meses: ", "Plazo en meses");
					
					try {
						Console.Clear();
						banco.CrearSolicitudCredito(tipo, id_cliente, monto_solicitado, plazo_meses);
						return;
					} catch (Exception ex) {
						Console.Clear();
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(ex.Message);
						Console.ResetColor();
						return;
					}
				} else{
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Entrada incorrecta, debe ingresar P o H");
					Console.ResetColor();
				}
			}
		}
		
		public static void EliminarSolicitud(Banco banco)
		{
			int idSolicitud = Validacion.ValidarEntero("Ingrese el ID de la solicitud a eliminar: ", "ID Solicitud");

			try
			{
				banco.EliminarSolicitudCredito(idSolicitud);
			}
			catch (EliminacionAsignadaException ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Error: " + ex.Message);
				Console.ResetColor();
			}
		}
		
		/* MENU */
		
		public static void ListarMenu()
		{
			Console.WriteLine("\n===== Menú =====");
			Console.WriteLine("1. Registrar Cliente");
			Console.WriteLine("2. Crear Solicitud de Credito");
			Console.WriteLine("3. Asignar Propiedad a Cliente");
			Console.WriteLine("4. Evaluar Solicitudes de Credito");
			Console.WriteLine("5. Eliminar Solicitud de Credito");
			Console.WriteLine("6. Eliminar Cliente");
			Console.WriteLine("7. Estadisticas");
			Console.WriteLine("8. Impresiones y Reportes");
			Console.WriteLine("9. Salir");
		}
		
		public static void ImpresionesYReportes(Banco banco)
		{
			
			Console.Clear();
			
			int opcion = 0;
			
			while (opcion != 5)
			{
				Console.WriteLine("\n=== Submenú de Impresiones y Reportes ===");
				Console.WriteLine("1. Listado de Clientes");
				Console.WriteLine("2. Listado de Solicitudes de Crédito");
				Console.WriteLine("3. Créditos Personales Aprobados");
				Console.WriteLine("4. Créditos Hipotecarios Aprobados");
				Console.WriteLine("5. Volver al menú principal");

				opcion = Validacion.ValidarEntero("Seleccione una opción: ", "Opción");

				switch (opcion)
				{
					case 1:
						Console.Clear();
						banco.ListarClientes();
						break;
					case 2:
						Console.Clear();
						banco.ListarDetallesCreditos();
						break;
					case 3:
						Console.Clear();
						banco.ListarSolicitudesPersonalesAprobadas();
						break;
					case 4:
						Console.Clear();
						banco.ListarSolicitudesHipotecarioAprobadas();
						break;
				}
			}
			
			Console.Clear();
		}
		
		public static void Estadisticas(Banco banco)
		{
			int opcion = 0;

			while (opcion != 4)
			{
				Console.WriteLine("\n=== Submenú de Estadisticas ===");
				Console.WriteLine("1. Monto total solicitado");
				Console.WriteLine("2. Cantidad de solicitudes por tipo");
				Console.WriteLine("3. Promedio edad clientes activos");
				Console.WriteLine("4. Volver al menú principal");

				opcion = Validacion.ValidarEntero("Seleccione una opción: ", "Opción");

				switch (opcion)
				{
					case 1:
						Console.Clear();
						double monto_total_creditos_aprobados = banco.MontoTotalCreditosAprobados();
						Console.WriteLine("Monto total de créditos aprobados: " + monto_total_creditos_aprobados);
						break;
					case 2:
						Console.Clear();
						Tuple<int, int> cantidad_credito_tipo = banco.CantidadCreditosPorTipo();
									
						Console.WriteLine("=== Cantidad de Creditos ===");
						Console.WriteLine("Creditos Personales: " + cantidad_credito_tipo.Item1);
						Console.WriteLine("Creditos Hipotecarios: " + cantidad_credito_tipo.Item2);
						break;
					case 3:
						Console.Clear();
						double promedio_edad_clientes_activos = banco.PromedioEdadClientesActivos();
						Console.WriteLine("Promedio de edad de clientes activos: " + promedio_edad_clientes_activos);
						break;
				}
			}
			
			Console.Clear();
		}
		
		
	}
}
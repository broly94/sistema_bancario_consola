/*
 * Creado por SharpDevelop.
 * Usuario: joaqu
 * Fecha: 13/10/2025
 * Hora: 15:33
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using sistema_bancario.Cliente;
using sistema_bancario.Banca;

namespace sistema_bancario.Helpers
{
	
	public static class Validacion
	{
		
		public static int ValidarEntero(string mensaje, string campo)
		{
			
			while (true)
			{
				Console.Write(mensaje);
				string entrada = Console.ReadLine();
				int valor;
				
				if (int.TryParse(entrada, out valor))
				{
					return valor;
				} else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: ingrese un valor valido para: " + campo);
					Console.ResetColor();
				}
			}
		}
		
		public static double ValidarDouble(string mensaje, string campo)
		{
			while(true)
			{
				Console.Write(mensaje);
				string entrada = Console.ReadLine();
				double valor;
				
				if (double.TryParse(entrada, out valor))
				{
					return valor;
				}else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Error: ingrese un valor valido para: " + campo);
					Console.ResetColor();
				}
			}
		}
		
		
		public static int ValidarEdad()
		{
			int edad;
			bool esValida;

			do
			{
				Console.Write("Por favor, ingrese su edad: ");
				esValida = int.TryParse(Console.ReadLine(), out edad);

				if (!esValida || edad < 18 || edad > 100)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Edad no válida. Debe estar entre 18 y 100 años.");
					Console.ResetColor();
					esValida = false;
				}

			} while (!esValida);

			return edad;
		}
		
		
		public static bool ValidarFinalizacion(string mensaje)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(mensaje);
			Console.ResetColor();
			
			string confirmacion = Console.ReadLine().ToLower();
			
			if (confirmacion != "s")
			{
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Operación cancelada.");
				Console.ResetColor();
				return false;
			}
			
			return true;
		}
				
	}
}

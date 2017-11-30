using System;

namespace TestPSL
{
    /// <summary>
    /// Prueba PSL
    /// Ivan Camilo Suarez Castro
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int digitos = 0;
            bool result = true;
            string entrada = string.Empty;

            do
            {
                Console.Write("\nEspacio entre Digitos(0 a 5): ");
                string lector = Console.ReadLine();
                result = int.TryParse(lector, out digitos);
                if (!result)
                    Console.WriteLine("Solo puede ingresar valores numericos");
                else if (digitos < 0 || digitos > 5)
                    Console.WriteLine("La cantidad de espacios debe estar entre 0 y 5");
            } while (!result || (digitos < 0 || digitos > 5));

            do
            {
                Console.WriteLine("Recuerde, la entrada es de la forma 'tamanio','numeros' (Ejemplo: Entrada: 2,12345 3,67890 0,0)");
                Console.Write("Entrada: ");
                entrada = Console.ReadLine();
                result = esValido(entrada);
            } while (!result);

            ImpresorLCD lcd = new ImpresorLCD(entrada, digitos);
            lcd.CargarLCD();
            lcd.Imprimir();

            Console.ReadLine();
        }

        static bool esValido(string entrada)
        {
            string[] cadena = entrada.Split(new char[] { ',' });

            if (cadena.Length != 2) return false;

            int tamanio, numeros;
            bool size = int.TryParse(cadena[0], out tamanio);
            bool numbers = int.TryParse(cadena[1], out numeros);

            return (size && numbers) ? true : false;
        }
    }
}

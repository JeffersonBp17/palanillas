using System;

//Lo siguientes son validaciones para el programa
class Validations
{

    public static int ObtenerEntrada(string prompt)
    {
        int input;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out input))
            {
                break;
            }
            else
            {
                Console.WriteLine("Entrada no válida. Intente nuevamente.");
            }
        }
        return input;
    }


    public static string ObtenerStrings(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();

    }
}

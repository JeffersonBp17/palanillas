using System;

class Validations
{

    public static int GetIntInput(string prompt)
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


    public static string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();

    }
}

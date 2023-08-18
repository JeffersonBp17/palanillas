using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace SaveUserInputToJson
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Menú principal:");
                Console.WriteLine("____________________");
                Console.WriteLine("1. Agregar un empleado");
                Console.WriteLine("2. Mostrar todos los Salarios");
                Console.WriteLine("3. Buscar empleado por ID");
                Console.WriteLine("4. Salir");



                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        List<Person> newPeople = GetUserInput();
                        SaveDataToFile(newPeople);
                        break;
                    case "2":
                        Console.Clear();
                        ShowAllRecords();
                        Console.WriteLine();
                        Console.WriteLine("Presione ENTER para volver al menu principal");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        Console.Write("Ingrese el ID del empleado que decea consultar: ");
                        string cedulaToSearch = Console.ReadLine();
                        ShowRecordsByCedula(cedulaToSearch);
                        Console.WriteLine();
                        Console.WriteLine("Presione ENTER para volver al menu principal");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Gracias por usar nuestro programa, ¡¡Vuelve Pronto!! \nPresione ENTER para salir");
                        Console.ReadKey();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida. \nPresione ENTER para volver al menu principal");
                        break;
                }
            }
        }

        static List<Person> LoadDataFromFile()
        {
            string filePath = "planilla.txt";

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Person>>(jsonData) ?? new List<Person>();
            }

            return new List<Person>();
        }

        static void SaveDataToFile(List<Person> people)
        {
            List<Person> existingPeople = LoadDataFromFile();
            existingPeople.AddRange(people);

            string jsonData = JsonConvert.SerializeObject(existingPeople, Formatting.Indented);
            string filePath = "planilla.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(jsonData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la información: " + ex.Message);
            }
        }

        // Resto del código igual que antes...
        static List<Person> GetUserInput()
        {
            List<Person> newPeople = new List<Person>();

            while (true)
            {
                //imformacion que se le solicita para agregar empleado
                Console.Clear();
                Console.WriteLine("Agregar empleado");

                //agrega numero de cedula
                Console.Clear();
                Console.Write("Ingrese el numero de cedula (o escriba 'salir' para finalizar): ");
                string cedula = Console.ReadLine();
                if (cedula.ToLower() == "salir")
                    break;

                //agrega el nombre del empleado
                Console.WriteLine("Ingrese el nombre  y apellido del empleado");
                string nombreCompleto = "";
                while (string.IsNullOrEmpty(nombreCompleto))
                {
                    Console.Write("Ingrese su nombre y apellidos: ");
                    nombreCompleto = Console.ReadLine();

                    if (string.IsNullOrEmpty(nombreCompleto))
                    {
                        Console.WriteLine("Error: Los datos solicitados no pueden estar vacíos. \nIngrese su nombre y apellido nuevamente nuevamente");
                        Console.WriteLine("Presione ENTER para continuar");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }


                //agrega el correo del empleado
                string correo = "";
                while (string.IsNullOrEmpty(correo) || !correo.Contains("@"))
                {
                    Console.Clear();
                    Console.Write("Ingrese su correo electrónico : ");
                    correo = Console.ReadLine();

                    if (string.IsNullOrEmpty(correo) || !correo.Contains("@"))
                    {
                        Console.WriteLine("Error: El correo electrónico debe contener el símbolo '@'. \nIngrese su correo nuevamente");
                        Console.WriteLine("Presione ENTER para continuar");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                //agrega las horas trabajadas del empleado
                Console.Clear();
                Console.Write("Ingrese las horas trabajadas: ");
                double Horas = Convert.ToDouble(Console.ReadLine());

                //agrega el salario por hora del empleado
                Console.Write("Ingrese el salario por hora: ");
                double PagoHoras = Convert.ToDouble(Console.ReadLine());

                //calcula el salario bruto
                int Rebajos = 10;
                Double salario = Horas * PagoHoras;
                Double Calculo = salario * Rebajos / 100;
                Double salarioNeto = salario - Calculo;

                newPeople.Add(new Person
                {
                    Cedula = cedula,
                    NombreCompleto = nombreCompleto,
                    Correo = correo,
                    HorasTrabajadas = Horas,
                    SalarioHora = PagoHoras,
                    SalarioBruto = salario,
                    Rebajos = Rebajos,
                    SalarioNeto = salarioNeto

                });

                Console.WriteLine("Datos agregados correctamente.\n");
            }

            return newPeople;
        }
        static void ShowAllRecords()
        {
            List<Person> allPeople = LoadDataFromFile();

            if (allPeople.Count == 0)
            {
                Console.WriteLine("No hay registros para mostrar.");
                return;
            }
            Console.WriteLine("Registros existentes:");
            foreach (var person in allPeople)
            {
                Console.WriteLine($"\nCédula: {person.Cedula}, Nombre: {person.NombreCompleto}, Correo: {person.Correo}, Horas trabajadas: {person.HorasTrabajadas}, Salario por hora: {person.SalarioHora} Salario bruto: {person.SalarioBruto}, Rebajos: {person.Rebajos}, Salario neto: {person.SalarioNeto}");
            }
        }

        static void ShowRecordsByCedula(string cedula)
        {
            List<Person> allPeople = LoadDataFromFile();
            bool found = false;

            foreach (var person in allPeople)
            {
                if (person.Cedula == cedula)
                {
                    Console.WriteLine($"\nCédula: {person.Cedula}, Nombre: {person.NombreCompleto}, Correo: {person.Correo}, Horas trabajadas: {person.HorasTrabajadas}, Salario por hora: {person.SalarioHora} Salario bruto: {person.SalarioBruto}, Rebajos: {person.Rebajos}, Salario neto: {person.SalarioNeto}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No se encontraron registros para la cédula proporcionada.");
            }
        }
    }

    class Person
    {
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public double HorasTrabajadas { get; set; }
        public double SalarioHora { get; set; }
        public double SalarioBruto { get; set; }
        public double Rebajos { get; set; }
        public double SalarioNeto { get; set; }

    }
}


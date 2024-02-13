//Examen I. Kevin Alfaro Lopez. Programacion II

using System.Collections.Concurrent;
using System.ComponentModel.Design;
using System.Security.Cryptography;

int opcion = 0;
int opcion2 = 0;
string opcions = string.Empty;
int indice = 0;
int contador1 = 0;
int tamano = 15;
int opcionNeutra = 0;
int [] numFact = new int[tamano];
string[] numPlaca = new string[tamano];
string[] fechas = new string[tamano];
string[] horas = new string[tamano];
string[] tipoVehiculo = new string[tamano];
int[] numCaseta = new int[tamano];
decimal[] montoPagar = new decimal[tamano];
decimal[] montoPagaCliente = new decimal[tamano];
decimal[] vuelto = new decimal[tamano];
bool opcionvalida = false;
bool encontrado = false;

Menu();

void Menu ()
{
    opcionvalida = false;
    while (!opcionvalida)
    {
        Console.WriteLine("*************** Sistema de peajes***************");
        Console.WriteLine("1- Inicializar vectores.");
        Console.WriteLine("2- Ingresar paso vehicular");
        Console.WriteLine("3- Consulta de vehículos x Número de Placa");
        Console.WriteLine("4- Modificar Datos Vehículos x número de Placa");
        Console.WriteLine("5- Reporte Todos los Datos de los vectores");
        Console.WriteLine("6- Salir");
        Console.WriteLine("************************************************");
        Console.WriteLine("Digite el numero de la opcion deseada:  ");
        try
        {
            opcion = int.Parse(Console.ReadLine());
            if (opcion<7 && opcion > 0)
            {
                switch (opcion)
                {
                    case 1: InicializarArreglos(); break;
                        case 2: IngresarDatos(); break;
                        case 3: ConsultaVehiculo(); break;
                        case 4: ModificaVehiculo(); break;
                        case 5: Reporte(); break;
                        case 6: opcionvalida = true; Console.WriteLine("Saliendo del sistema, cierre la ventana para salir");
                        break;
                }
            }
            else
            {
                Console.WriteLine("La opcion digitada no corresponde a ninguna de las opciones validas. Presione cualquier tecla para intentarlo de nuevo");
                Console.ReadKey();
                Console.Clear();
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("La opcion digitada no corresponde a ninguna de las opciones validas. Presione cualquier tecla para intentarlo de nuevo");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

void InicializarArreglos()
{
    numFact = Enumerable.Repeat(0, tamano).ToArray<int>();
    numPlaca = Enumerable.Repeat(" ", tamano).ToArray<string>();
    fechas =  Enumerable.Repeat(" ", tamano).ToArray<string>();
    horas = Enumerable.Repeat(" ", tamano).ToArray<string>();
    tipoVehiculo = Enumerable.Repeat(" ", tamano).ToArray<string>();
    numCaseta = Enumerable.Repeat(0, tamano).ToArray<int>();
    montoPagar = Enumerable.Repeat(0m, tamano).ToArray<decimal>();
    montoPagaCliente = Enumerable.Repeat(0m, tamano).ToArray<decimal>();
    vuelto = Enumerable.Repeat(0m, tamano).ToArray<decimal>();
    Console.WriteLine("Arreglos inicializados correctamente. Presione enter para continuar");
    Console.ReadKey();
    Console.Clear();
}
void IngresarDatos() 
{

    Console.WriteLine("Digite los datos segun se le soliciten.");
    opcionvalida = false;
    opcions = string.Empty;

    while (!opcionvalida)
    {
        Console.WriteLine("Desea registrar datos? Digite 'si' para ingresar un dato o 'no' para salir.");
        opcions = Console.ReadLine().ToLower();

        if (opcions.Equals("si"))
        {
            if (indice < numFact.Length)
            {

                //Numero de facura
                while (!opcionvalida)
                {
                    Console.WriteLine("Digite el numero de factura, recuerde que no se pueden incluir letras.");
                    opcions = Console.ReadLine();
                    if (int.TryParse(opcions, out opcionNeutra))
                    {
                        numFact[contador1] = opcionNeutra;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ingreso letras donde no debía. Presione alguna tecla para intentarlo de nuevo.");
                        Console.ReadKey();
                    }
                }
                opcions = "";

                //Numero de placa
                Console.WriteLine("Digite el numero de placa");
                opcions = Console.ReadLine();
                numPlaca[contador1] = opcions;

                //Fecha
                bool fechaValida = false;
                DateTime fechaIngresada = DateTime.MinValue;
                while (!fechaValida)//Validador de formato de fecha y asignador de la misma en el vector.
                {
                    Console.WriteLine("Por favor, ingresa una fecha en formato dd/MM/yyyy:");
                    string fechaTexto = Console.ReadLine();

                    try
                    {
                        fechaIngresada = DateTime.ParseExact(fechaTexto, "dd/MM/yyyy", null);
                        fechaValida = true;
                        fechas[contador1] = fechaIngresada.ToShortDateString();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Formato de fecha incorrecto. Por favor, intenta de nuevo.");
                    }
                }

                //Hora
                string formatoHora = "HH:mm"; // Formato de hora deseado (24 horas)
                DateTime horaIngresada; bool formatoCorrecto = false;
                while (!formatoCorrecto)
                {
                    Console.WriteLine("Por favor ingrese la hora en formato HH:mm (24 horas): ");
                    string hora = Console.ReadLine();

                    if (DateTime.TryParseExact(hora, formatoHora, null, System.Globalization.DateTimeStyles.None, out horaIngresada))
                    {
                        formatoCorrecto = true;
                        Console.WriteLine("Hora ingresada correctamente: " + horaIngresada.ToString("HH:mm"));
                        horas[contador1] = hora;
                    }
                    else
                    {
                        Console.WriteLine("Formato de hora incorrecto. Por favor, inténtelo nuevamente.");
                    }
                }

                //Tipo vehiculo y monto a pagar
                Console.WriteLine("Digite el numero del tipo de vehiculo segun corresponda:\n 1= Moto 2= Vehículo Liviano 3 =Camión o Pesado  4=Autobús.");
                opcion = 0;
                opcionvalida = false;
                do
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out opcion))
                    {
                        switch (opcion)
                        {
                            case 1:
                                tipoVehiculo[contador1] = "1-Moto";
                                montoPagar[contador1] = 500;
                                opcionvalida = true;
                                break;
                            case 2:
                                tipoVehiculo[contador1] = "2-Vehículo Liviano";
                                montoPagar[contador1] = 700;
                                opcionvalida = true;
                                break;
                            case 3:
                                tipoVehiculo[contador1] = "3-Camión o Pesado";
                                montoPagar[contador1] = 2700;
                                opcionvalida = true;
                                break;
                            case 4:
                                tipoVehiculo[contador1] = "4-Autobús.";
                                montoPagar[contador1] = 3700;
                                opcionvalida = true;
                                break;
                            default:
                                Console.WriteLine("Opción inválida. Por favor, ingrese un número válido.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                    }
                } while (!opcionvalida);

                opcionvalida = false;

                //Numero caseta
                while (!opcionvalida)
                {
                    Console.WriteLine("Digite el numero de caseta, recuerde que no se pueden incluir letras. El numero puede ser el 1, 2 o 3");
                    opcions = Console.ReadLine();
                    if (int.TryParse(opcions, out opcionNeutra))
                    {
                        if (opcionNeutra<4 && opcionNeutra>0)
                        {
                            switch (opcionNeutra)
                            {
                                case 1:
                                    numCaseta[contador1] = 1;
                                    break;
                                case 2:
                                    numCaseta[contador1] = 2;
                                    break;
                                case 3:
                                    numCaseta[contador1] = 3;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("El numero digitado esta fuera el rango valido");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Ingreso letras donde no debía. Presione alguna tecla para intentarlo de nuevo.");
                        Console.ReadKey();
                    }
                }

                //Monto paga cliente
                decimal pago;
                bool pagoCliente = false;
                while (!pagoCliente)
                {
                    Console.WriteLine("Digite el monto con el que el cliente esta pagando");
                    pago = decimal.Parse(Console.ReadLine());
                    if (pago > montoPagar[contador1])
                    {
                        pagoCliente = true;
                        montoPagaCliente[contador1] = pago;
                        Console.WriteLine($"El pago del cliente es: {pago}");
                    }
                    else
                    {
                        Console.WriteLine("El pago del cliente no puede ser menor al monto a pagar. Por favor, inténtelo nuevamente.");
                    }
                }

                //Vuelto
                vuelto[contador1] = montoPagaCliente[contador1] - montoPagar[contador1];
                Console.WriteLine($"El vuelto para el cliente es de: {vuelto[contador1]}");



            }
            else
            {
                Console.WriteLine("Los vectores se encuentran llenos, no se pueden incluir mas datos");
                Console.WriteLine("Presione una tecla para salir");
                Console.ReadKey();
                Console.Clear();
                break;
            }
        }
        else if (opcions.Equals("no"))
        {
            Console.WriteLine("Saliendo del sistema de pagos. Digite cualquier tecla para salir.");
            Console.ReadKey();
            Console.Clear();
            break;
        }
        else
        {
            Console.WriteLine("la opcion digitada no corresponde a ninguna de las opciones validas. Presione una tecla para intentarlo de nuevo");
            Console.ReadKey();
            
        }
        
    }
}
void ConsultaVehiculo()
{
    encontrado = false;
    Console.WriteLine("Digite el numero de placa a consultar");
    opcions = Console.ReadLine();

    for (int i = 0; i < numPlaca.Length; i++)
    {
        if (opcions == numPlaca[i])
        {
            Console.WriteLine("Dato econtrado, presione una tecla para desplegar la informacion");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"Numero de factura: {numFact[i]}");
            Console.WriteLine($"Fecha:             {fechas[i]}          Hora: {horas[i]}");
            Console.WriteLine($"                                                               ");
            Console.WriteLine($"Tipo de vehiculo:  {tipoVehiculo[i]}");
            Console.WriteLine($"Numero de placa: {numPlaca[i]} ");
            Console.WriteLine($"Monto a pagar: {montoPagar[i]}");
            Console.WriteLine($"Paga con: {montoPagaCliente[i]}");
            Console.WriteLine($"Vuelto: {vuelto[i]}");
            encontrado = true;
            Console.WriteLine("Presione una tecla para salir");
            Console.ReadKey();
            Console.Clear();
            break;
        }
    }
    if (!encontrado)
    {
        Console.WriteLine("El numero de placa no se encuentra registrado. Presione una tecla para salir y por favor ingrese nuevamente.");
        Console.ReadKey();
        Console.Clear();
    }
    opcion2 = 0;
}

void ModificaVehiculo() 
{
    encontrado = false;
    Console.WriteLine("Digite el numero de placa a consultar");
    opcions = Console.ReadLine();

    for (int i = 0; i < numPlaca.Length; i++)
    {
        if (opcions == numPlaca[i])
        {
            Console.WriteLine("Dato econtrado, presione una tecla para desplegar la informacion");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"1-Numero de factura: {numFact[i]}");
            Console.WriteLine($"2-Fecha:             {fechas[i]}          3-Hora: {horas[i]}");
            Console.WriteLine($"                                                               ");
            Console.WriteLine($"4-Tipo de vehiculo:  {tipoVehiculo[i]}");
            Console.WriteLine($"5-Numero de placa: {numPlaca[i]} ");
            Console.WriteLine($"-Monto a pagar: {montoPagar[i]}");
            Console.WriteLine($"6-Paga con: {montoPagaCliente[i]}");
            Console.WriteLine($"Vuelto: {vuelto[i]}");
            Console.WriteLine("Digite el numero de la opcion a modificar:");
            opcion2 = int.Parse(Console.ReadLine());
            Console.Clear();
        }
    }
    if (!encontrado)
    {
        Console.WriteLine("El numero de placa no se encuentra registrado. Presione una tecla para salir y por favor ingrese nuevamente.");
        Console.ReadKey();
        Console.Clear();
    }
    opcion2 = 0;
}

void Reporte()
{
    Console.WriteLine($"# factura   Fecha/Hora  Tipo vehiculo  Caseta  MontoPagar   PagaCon   Vuelto");
    Console.WriteLine("-------------------------------------------------------------------------------");
    for (int i = 0; i < numFact.Length; i++)
    {
        Console.WriteLine($"{numFact[i]} / {fechas[i]}{horas[i]} / {tipoVehiculo[i]} / {numCaseta[i]} / {montoPagar[i]} / {montoPagaCliente[i]} / {vuelto[i]}");
    }
    Console.WriteLine("Digite cualquier tecla para salir.");
    Console.ReadLine();
    Console.Clear();
    
}

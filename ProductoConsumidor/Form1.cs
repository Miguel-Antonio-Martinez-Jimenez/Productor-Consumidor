namespace ProductoConsumidor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Declaraciones globales:
        private int[] Buffer = new int[10]; // Array que actúa como buffer para almacenar números primos.
        private int Contador = 0; // Lleva el control del número de elementos en el buffer.
        private int ContadorNumeroPrimario = 0; // Lleva el control de cuántos números primos se han generado.
        private bool Final = false; // Indica si el proceso de generación de números primos ha terminado.
        private object Lock = new object(); // Objeto para manejar el acceso sincronizado al buffer.

        // Evento para iniciar los hilos productor y consumidor al hacer clic en el botón.
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            // Crear hilos para productor y consumidor.
            Thread productorThread = new Thread(Productor);
            Thread consumidorThread = new Thread(Consumidor);

            // Iniciar los hilos.
            productorThread.Start();
            consumidorThread.Start();
        }

        // Método para verificar si un número es primo.
        private bool VerificarNumeroPrimo(int numero)
        {
            if (numero <= 1) return false; // Los números <= 1 no son primos.
            for (int i = 2; i <= Math.Sqrt(numero); i++) // Verificar divisibilidad hasta la raíz cuadrada del número.
            {
                if (numero % i == 0) // Si es divisible, no es primo.
                    return false;
            }
            return true; // Si no se encontró divisor, el número es primo.
        }

        // Método del productor: genera números primos y los coloca en el buffer.
        private void Productor()
        {
            int numero = 2; // Comenzar desde el número 2.
            while (ContadorNumeroPrimario < 1000) // Generar hasta 1000 números primos.
            {
                if (VerificarNumeroPrimo(numero)) // Si el número es primo:
                {
                    lock (Lock) // Bloquear el acceso al buffer.
                    {
                        while (Contador == 10) // Si el buffer está lleno, esperar.
                        {
                            Monitor.Wait(Lock);
                        }

                        // Agregar el número primo al buffer.
                        Buffer[Contador] = numero;
                        Contador++;
                        ContadorNumeroPrimario++;

                        // Actualizar la interfaz de usuario con el nuevo número en el buffer.
                        Invoke((MethodInvoker)delegate
                        {
                            TxtBoxBuffer.ScrollBars = ScrollBars.Vertical;
                            TxtBoxBuffer.AppendText($"Numero {Contador}: {numero}\r\n");
                        });

                        // Notificar al consumidor si el buffer está lleno.
                        if (Contador == 10)
                        {
                            Monitor.PulseAll(Lock);
                        }
                    }
                }
                numero++; // Incrementar al siguiente número.
            }
            Final = true; // Indicar que el proceso ha terminado.
        }

        // Método del consumidor: consume números del buffer y los procesa.
        private void Consumidor()
        {
            while (true)
            {
                lock (Lock) // Bloquear el acceso al buffer.
                {
                    // Esperar si el buffer no está lleno y el proceso no ha terminado.
                    while (Contador < 10 && !Final)
                    {
                        Monitor.Wait(Lock);
                    }

                    // Si el buffer está lleno, procesar los números.
                    if (Contador == 10)
                    {
                        // Actualizar la interfaz de usuario mostrando los números consumidos.
                        Invoke((MethodInvoker)delegate
                        {
                            TxtBoxBufferList.ScrollBars = ScrollBars.Vertical;
                            TxtBoxBufferList.AppendText("Consumidor Imprimió: \r\n");
                            for (int i = 0; i < 10; i++)
                            {
                                TxtBoxBufferList.AppendText(Buffer[i] + " ");
                            }
                            TxtBoxBufferList.AppendText("\r\n\r");
                        });

                        // Vaciar el buffer.
                        Contador = 0;

                        // Notificar al productor que hay espacio disponible.
                        Monitor.PulseAll(Lock);
                    }

                    // Salir si el productor ha terminado y el buffer está vacío.
                    if (Final && ContadorNumeroPrimario == 1000 && Contador == 0)
                        break;
                }
            }
        }
    }
}

/*
 * Alumnos:
 * Aguilar Medina Francisco Antonio
 * Jeronimo Trinidad Eduardo Isai
 * Martin Tamay Johan Ivan
 * Martinez Jimenez Miguel Antonio
 */

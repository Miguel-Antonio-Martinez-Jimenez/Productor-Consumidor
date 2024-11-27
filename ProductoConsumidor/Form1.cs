namespace ProductoConsumidor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Declaraciones globales:
        private int[] Buffer = new int[10]; // Array que act�a como buffer para almacenar n�meros primos.
        private int Contador = 0; // Lleva el control del n�mero de elementos en el buffer.
        private int ContadorNumeroPrimario = 0; // Lleva el control de cu�ntos n�meros primos se han generado.
        private bool Final = false; // Indica si el proceso de generaci�n de n�meros primos ha terminado.
        private object Lock = new object(); // Objeto para manejar el acceso sincronizado al buffer.

        // Evento para iniciar los hilos productor y consumidor al hacer clic en el bot�n.
        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            // Crear hilos para productor y consumidor.
            Thread productorThread = new Thread(Productor);
            Thread consumidorThread = new Thread(Consumidor);

            // Iniciar los hilos.
            productorThread.Start();
            consumidorThread.Start();
        }

        // M�todo para verificar si un n�mero es primo.
        private bool VerificarNumeroPrimo(int numero)
        {
            if (numero <= 1) return false; // Los n�meros <= 1 no son primos.
            for (int i = 2; i <= Math.Sqrt(numero); i++) // Verificar divisibilidad hasta la ra�z cuadrada del n�mero.
            {
                if (numero % i == 0) // Si es divisible, no es primo.
                    return false;
            }
            return true; // Si no se encontr� divisor, el n�mero es primo.
        }

        // M�todo del productor: genera n�meros primos y los coloca en el buffer.
        private void Productor()
        {
            int numero = 2; // Comenzar desde el n�mero 2.
            while (ContadorNumeroPrimario < 1000) // Generar hasta 1000 n�meros primos.
            {
                if (VerificarNumeroPrimo(numero)) // Si el n�mero es primo:
                {
                    lock (Lock) // Bloquear el acceso al buffer.
                    {
                        while (Contador == 10) // Si el buffer est� lleno, esperar.
                        {
                            Monitor.Wait(Lock);
                        }

                        // Agregar el n�mero primo al buffer.
                        Buffer[Contador] = numero;
                        Contador++;
                        ContadorNumeroPrimario++;

                        // Actualizar la interfaz de usuario con el nuevo n�mero en el buffer.
                        Invoke((MethodInvoker)delegate
                        {
                            TxtBoxBuffer.ScrollBars = ScrollBars.Vertical;
                            TxtBoxBuffer.AppendText($"Numero {Contador}: {numero}\r\n");
                        });

                        // Notificar al consumidor si el buffer est� lleno.
                        if (Contador == 10)
                        {
                            Monitor.PulseAll(Lock);
                        }
                    }
                }
                numero++; // Incrementar al siguiente n�mero.
            }
            Final = true; // Indicar que el proceso ha terminado.
        }

        // M�todo del consumidor: consume n�meros del buffer y los procesa.
        private void Consumidor()
        {
            while (true)
            {
                lock (Lock) // Bloquear el acceso al buffer.
                {
                    // Esperar si el buffer no est� lleno y el proceso no ha terminado.
                    while (Contador < 10 && !Final)
                    {
                        Monitor.Wait(Lock);
                    }

                    // Si el buffer est� lleno, procesar los n�meros.
                    if (Contador == 10)
                    {
                        // Actualizar la interfaz de usuario mostrando los n�meros consumidos.
                        Invoke((MethodInvoker)delegate
                        {
                            TxtBoxBufferList.ScrollBars = ScrollBars.Vertical;
                            TxtBoxBufferList.AppendText("Consumidor Imprimi�: \r\n");
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

                    // Salir si el productor ha terminado y el buffer est� vac�o.
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CustoEfetivoTotal
{
    public abstract class CalculadoraCET
    {
        // nLoop para ver quantas vezes executamos o laço de cálculo.
        protected int nLoop { get; set; }
        public Stopwatch _stopwatch { get; set; } = new Stopwatch();

        protected double cet { get; set; }

        public void Display()
        {
            Console.WriteLine($"Número de loops: {nLoop}");
            Console.WriteLine($"Tempo decorrido em Ticks: {_stopwatch.ElapsedTicks}");
            Console.WriteLine($"Valor do CET: {cet}");
        }

        public void Reset()
        {
            nLoop = 0;
            _stopwatch.Reset();
        }
    }
}

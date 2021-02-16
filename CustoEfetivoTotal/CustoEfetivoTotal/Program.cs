using System;
using System.Collections.Generic;

namespace CustoEfetivoTotal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== Chamando CetSolucaoPronta ==========");

            // Configuramos o cálculo
            var solucaoPronta = new CetSolucaoPronta(950, 207.79, 5, DateTime.Parse("02/01/2017"), DateTime.Parse("02/02/2017"));

            solucaoPronta.GetCET();

            solucaoPronta.Display();

            Console.WriteLine("========== Fim do CetSolucaoPronta ========== ");

            Console.WriteLine();

            Console.WriteLine("========== Chamando CetSolucaoSecantes ==========");

            // Configuramos o cálculo

            var listaParcelas = new List<Parcelas>();

            listaParcelas.Add(
                new Parcelas
                {
                    DataParcela = DateTime.Parse("02/02/2017"),
                    ValorParcela = 207.79
                });

            for (var j = 1; j < 5; j++)
            {
                var newData = listaParcelas[0].DataParcela.AddMonths(j);
                listaParcelas.Add(new Parcelas
                {
                    DataParcela = newData,
                    ValorParcela = 207.79
                });
            }

            var solucaoSecantes = new CetSolucaoSecantes(950, DateTime.Parse("02/01/2017"), listaParcelas);

            solucaoSecantes.GetCET();

            solucaoSecantes.Display();

            Console.WriteLine("========== Fim do CetSolucaoPronta ========== ");

            Console.WriteLine();

            Console.WriteLine("========== Verificando ========== ");

            Console.WriteLine($"Proximidade da solução para CetSolucaoPronta: {solucaoPronta.Verify()}");
            Console.WriteLine($"Proximidade da solução para CetSolucaoSecantes: {solucaoSecantes.Verify()}");

            Console.WriteLine("============================================= ");

            Console.WriteLine();

            Console.WriteLine("========== Média de Ticks em 1000 ========== ");

            var cetSolucaoProntaN = 0.0;
            var solucaoSecantesN = 0.0;

            for (var j = 1; j<= 1000 ; j++)
            {
                solucaoPronta.GetCET();
                solucaoSecantes.GetCET();

                cetSolucaoProntaN += solucaoPronta._stopwatch.ElapsedTicks;
                solucaoSecantesN += solucaoSecantes._stopwatch.ElapsedTicks;

                solucaoPronta.Reset();
                solucaoSecantes.Reset();
            }

            cetSolucaoProntaN /= 1000;
            solucaoSecantesN /= 1000;

            Console.WriteLine($" Ticks médio em CetSolucaoPronta: {cetSolucaoProntaN}");
            Console.WriteLine($" Ticks médio em CetSolucaoSecantes: {solucaoSecantesN}");


            Console.WriteLine("============================================ ");
        }
    }
}

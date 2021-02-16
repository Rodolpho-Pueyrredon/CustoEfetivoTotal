using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CustoEfetivoTotal
{
    public class CetSolucaoPronta : CalculadoraCET, ICalculadoraCET
    {
        

    private double _valorFinanciado { get; set; }
        private double _valorParcela { get; set; }
        private int _prazo { get; set; }
        private DateTime _dataContrato { get; set; }
        private DateTime _dataPrimeiroVencimento { get; set; }

        public double precision = 0.000000001;

        public CetSolucaoPronta(
            double valorFinanciado,
            double valorParcela,
            int prazo,
            DateTime dataContrato,
            DateTime dataPrimeiroVencimento
            )
        {
            _valorFinanciado = valorFinanciado;
            _valorParcela = valorParcela;
            _prazo = prazo;
            _dataContrato = dataContrato;
            _dataPrimeiroVencimento = dataPrimeiroVencimento;
        }

        public void GetCET()
        {
            _stopwatch.Start();

            cet = _GetCET();

            _stopwatch.Stop();
        }

        private double _GetCET()
        {
            double valorMaximoCet = 10000;
            double cet = 0;
            nLoop = 0;

            while (true)
            {
                nLoop += 1;
                DateTime c;
                DateTime dj;
                double total = 0.0;

                for (int j = 0; j < _prazo; j++)
                {
                    dj = _dataPrimeiroVencimento;
                    if (j != 0)
                    {
                        c = _dataPrimeiroVencimento;
                        dj = c.AddMonths(j);
                    }
                    double days = (dj - _dataContrato).Days;
                    // Adicioando parcelas da forma: _valorParcela / (1 + x)^(days/365)
                    total += _valorParcela / Math.Pow(1 + cet, days / 365);
                }

                cet += precision;

                if (cet >= valorMaximoCet)
                {
                    return -1;
                }
                if (total - _valorFinanciado <= 0)
                {
                    break;
                }
                else
                {
                    cet *= total / _valorFinanciado;
                }
            }

            return cet * 100;
        }

        public double Verify()
        {
            DateTime c;
            DateTime dj;
            double total = 0.0;

            for (int j = 0; j < _prazo; j++)
            {
                dj = _dataPrimeiroVencimento;
                if (j != 0)
                {
                    c = _dataPrimeiroVencimento;
                    dj = c.AddMonths(j);
                }
                double days = (dj - _dataContrato).Days;
                // Adicioando parcelas da forma: _valorParcela / (1 + x)^(days/365)
                total += _valorParcela / Math.Pow(1 + cet/100, days / 365);
            }

            total -= _valorFinanciado;

            return total;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CustoEfetivoTotal
{
    public class CetSolucaoSecantes : CalculadoraCET, ICalculadoraCET
    {

        private double _valorFinanciado { get; set; }
        private DateTime _dataContrato { get; set; }
        private IEnumerable<Parcelas> _listParcelas { get; set; }

        public double precision = 0.000000001;

        public CetSolucaoSecantes(
            double valorFinanciado,
            DateTime dataContrato,
            IEnumerable<Parcelas> listParcelas
            )
        {
            _valorFinanciado = valorFinanciado;
            _dataContrato = dataContrato;
            _listParcelas = listParcelas;
        }

        public void GetCET()
        {
            _stopwatch.Start();

            cet = _GetCET();

            _stopwatch.Stop();
        }

        private double _GetCET()
        {

            double init0 = 0.0;
            double init1 = 1.0;

            double nV = 0.0;

            nLoop = 0;

            while (true)
            {
                nLoop += 1;
                var Func0 = ValueExpression(init0);
                var Func1 = ValueExpression(init1);

                // Aqui está a formula do método das secantes
                nV = init1 - (((init1 - init0) / (Func1 - Func0)) * Func1);

                if (Math.Abs(nV - init1) <= precision)
                {
                    break;
                }

                init0 = init1;
                init1 = nV;
            }

            return nV * 100;
        }

        private double ValueExpression(double cet)
        {
            var total = 0.0;

            foreach (var parcela in _listParcelas)
            {
                double days = (parcela.DataParcela - _dataContrato).Days;
                // Adicioando parcelas da forma: _valorParcela * (1 + x)^(-days/365). Note o expoente negativo.
                total += parcela.ValorParcela * Math.Pow(1 + cet, -days / 365);
            }

            total -= _valorFinanciado;

            return total;
        }

        public double Verify()
        {
            return ValueExpression(cet/100);
        }
    }

    public class Parcelas
    {
        public DateTime DataParcela;
        public double ValorParcela;
    }
}

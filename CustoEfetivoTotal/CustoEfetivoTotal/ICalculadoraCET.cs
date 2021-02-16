using System;
using System.Collections.Generic;
using System.Text;

namespace CustoEfetivoTotal
{
    interface ICalculadoraCET
    {
        void GetCET();
        void Display();
        void Reset();

        double Verify();
    }
}

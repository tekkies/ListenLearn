using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;


namespace ListenLearn.Listen.Core
{
    public class AforgeFftAnalyser : Analyser
    {
        public Complex[] Analyse(Double[] input)
        {
            var complexArray = ToComplex(input);
            FourierTransform.FFT(complexArray, FourierTransform.Direction.Forward);
            return complexArray;
        }

        private Complex[] ToComplex(double[] input)
        {
            var complex = new Complex[input.Length];
            for(int i=0;i<input.Length;i++)
            {
                complex[i] = new Complex(input[i], 0);
            }
            return complex;
        }
    }
}

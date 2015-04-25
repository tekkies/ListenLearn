using System;
using AForge.Math;

namespace ListenLearn.Listen.Core
{
    public class AforgeFftAnalyser : Analyser
    {
        public Double[] Analyse(Double[] input)
        {
            var complexArray = ToComplex(input);
            FourierTransform.FFT(complexArray, FourierTransform.Direction.Forward);
            return ExtractRealSpectrum(complexArray);
        }

        private double[] ExtractRealSpectrum(Complex[] input)
        {
            var output = new double[input.Length/2];
            for (var i = 0; i < input.Length/2; i++)
            {
                output[i] = input[i].Magnitude;
            }
            return output;
        }

        private Complex[] ToComplex(double[] input)
        {
            var complex = new Complex[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                complex[i] = new Complex(input[i], 0);
            }
            return complex;
        }

        public int GetFrequency(int element, int sampleRate, int sampleSize)
        {
            return (element*sampleRate)/(sampleSize*2);
        }
    }
}
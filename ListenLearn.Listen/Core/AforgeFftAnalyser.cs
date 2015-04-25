using System;
using AForge.Math;

namespace ListenLearn.Listen.Core
{
    public class AforgeFftAnalyser : Analyser
    {
        public Double[] Analyse(Double[] waveform)
        {
            var complexArray = ToComplex(waveform);
            FourierTransform.FFT(complexArray, FourierTransform.Direction.Forward);
            return ExtractRealSpectrum(complexArray);
        }

        private double[] ExtractRealSpectrum(Complex[] spectrumIn)
        {
            var spectrumOut = new double[spectrumIn.Length/2];
            for (var i = 0; i < spectrumIn.Length/2; i++)
            {
                spectrumOut[i] = spectrumIn[i].Magnitude;
            }
            return spectrumOut;
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
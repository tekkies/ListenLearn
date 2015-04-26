using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenLearn.Listen.Core
{
    public class WaveUtils
    {
        public static void MixSinWave(double[] waveform, double frequency, double amplitude, double offset)
        {
            for (var i = 0; i < waveform.Length; i++)
            {
                waveform[i] += amplitude * Math.Sin((frequency * Math.PI * 2 * i) / waveform.Length + offset);
            }
        }
    }
}

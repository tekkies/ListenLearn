using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenLearn.Listen.Core
{
    public class AnalyseUtils
    {
        public static int GetPeakElement(double[] output)
        {
            int peakElement = -1;
            double peakValue = 0;
            for (int element = 0; element < output.Length; element++)
            {
                if (output[element] > peakValue)
                {
                    peakElement = element;
                    peakValue = output[element];
                }
            }
            return peakElement;
        }
    }
}

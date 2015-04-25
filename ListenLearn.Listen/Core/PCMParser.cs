using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenLearn.Listen.Core
{
    public class PcmParser
    {
        public double[] data;
        public void Parse(byte[] bytes, int sampleCount)
        {
            data = new double[sampleCount];
            for (int byteIndex = 0x0; byteIndex < sampleCount*2; byteIndex += 2)
            {
                data[byteIndex/2] = BitConverter.ToInt16(bytes, byteIndex);
            }
        }
    }
}
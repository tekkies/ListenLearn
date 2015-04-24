using Android.Media;

namespace ListenLearn.Client.Android
{
    internal class AudioSampler
    {
        public const int SampleRateInHz = 44100;
        private const ChannelIn ChannelConfig = ChannelIn.Mono;
        private const Encoding AudioFormat = Encoding.Pcm16bit;
        private const AudioSource AudioSource = global::Android.Media.AudioSource.Mic;
        public byte[] AudioBuffer;
        public int BytesRead;

        public void Capture()
        {
            PrepareBuffer();
            using (var audioRecord = new AudioRecord(
                AudioSource,
                SampleRateInHz,
                ChannelConfig,
                AudioFormat,
                AudioBuffer.Length
                ))
            {
                try
                {
                    audioRecord.StartRecording();
                    BytesRead = audioRecord.Read(AudioBuffer, 0, AudioBuffer.Length);

                }
                finally
                {
                    audioRecord.Stop();
                } 
            }
        }

        private void PrepareBuffer()
        {
            var bufferSize = AudioRecord.GetMinBufferSize(SampleRateInHz, ChannelConfig, AudioFormat);
            AudioBuffer = new byte[bufferSize];
            for (int i = 0; i < AudioBuffer.Length; i++)
            {
                AudioBuffer[i] = 0;
            }
        }
    }
}
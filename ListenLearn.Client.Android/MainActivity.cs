using System;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Widget;
using Java.IO;

namespace ListenLearn.Client.Android
{
    [Activity(Label = "ListenLearn.Client.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
                SaveAudioSample();
            };
        }

        private void SaveAudioSample()
        {
            var audioSampler = new AudioSampler();
            audioSampler.Capture();
            Save(audioSampler);
        }

        private void Save(AudioSampler audioSampler)
        {
            var file = new File(GetExternalFilesDir(null), DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + "." + AudioSampler.SampleRateInHz + ".sample");
            using (var outputStrem = new Java.IO.FileOutputStream(file))
            {
                outputStrem.Write(audioSampler.AudioBuffer, 0, audioSampler.BytesRead);
            }
        }
    }

}
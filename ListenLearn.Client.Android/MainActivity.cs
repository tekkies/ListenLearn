using System;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace ListenLearn.Client.Android
{
    [Activity(Label = "ListenLearn.Client.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, View.IOnClickListener
    {
        private int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            FindViewById<Button>(Resource.Id.ButtonA).SetOnClickListener(this);
            FindViewById<Button>(Resource.Id.ButtonB).SetOnClickListener(this);
            FindViewById<Button>(Resource.Id.ButtonC).SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v is Button)
            {
                SaveAudioSample(((Button)v).Text);    
            }
        }

        private void SaveAudioSample(string id)
        {
            var audioSampler = new AudioSampler();
            audioSampler.Capture();
            Save(audioSampler, id);
        }

        private void Save(AudioSampler audioSampler, string id)
        {
            var file = new File(GetExternalFilesDir(null), DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss-fff") + "." + id + "." + AudioSampler.SampleRateInHz + ".sample");
            using (var outputStrem = new Java.IO.FileOutputStream(file))
            {
                outputStrem.Write(audioSampler.AudioBuffer, 0, audioSampler.BytesRead);
            }
        }

    }

}
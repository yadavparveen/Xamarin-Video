using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Media;

[assembly: ExportRenderer(typeof(Moments.VideoPage), typeof(Moments.Droid.VideoPage))]
namespace Moments.Droid
{
  public  class VideoPage : PageRenderer, TextureView.ISurfaceTextureListener
    {
        global::Android.Views.View view;
        //global::Android.Hardware.Camera camera;
        global::Android.Widget.Button recordVideoButton;
        Activity activity;
        MediaRecorder recorder;
        TextureView textureView;
        public VideoPage()
        {

           

            
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                activity = this.Context as Activity;
                view = activity.LayoutInflater.Inflate(Resource.Layout.CameraLayout, this, false);
                // cameraType = CameraFacing.Back;

                textureView = view.FindViewById<TextureView>(Resource.Id.textureVideoView);
                textureView.SurfaceTextureListener = this;

                recordVideoButton = view.FindViewById<global::Android.Widget.Button>(Resource.Id.Record);
                recordVideoButton.Click += TakeVideoButtonTapped;

              

                AddView(view);
            }
            catch (Exception ex)
            {
                //Xamarin.Insights.Report (ex);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }

        private async void TakeVideoButtonTapped(object sender, EventArgs e)
        {
     
                var video = FindViewById<VideoView>(Resource.Id.MyVideoView);
                video.StopPlayback();
                string path = "Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + /test.mp4";

                recorder = new MediaRecorder();
                recorder.SetVideoSource(VideoSource.Camera);
                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.Default);
                recorder.SetVideoEncoder(VideoEncoder.Default);
                recorder.SetAudioEncoder(AudioEncoder.Default);
                recorder.SetOutputFile(path);
                recorder.SetPreviewDisplay(video.Holder.Surface);
                recorder.Prepare();
                recorder.Start();
           
        }
    }
}
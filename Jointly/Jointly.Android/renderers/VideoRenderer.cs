using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Jointly.Droid.Renderers;
using Jointly.Views.CustomUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Video), typeof(VideoRenderer))]
namespace Jointly.Droid.Renderers
{
    class VideoRenderer : ViewRenderer<Video, FrameLayout>,
        TextureView.ISurfaceTextureListener,
        ISurfaceHolderCallback
    {
        public VideoRenderer(Context context) : base(context) { }

        private bool _isCompletionSubscribed = false;

        private FrameLayout _mainFrameLayout = null;

        private Android.Views.View _mainVideoView = null;
        private Android.Views.View _placeholder = null;

        private MediaPlayer _videoPlayer = null;
        internal MediaPlayer VideoPlayer
        {
            get
            {
                if (_videoPlayer == null)
                {
                    _videoPlayer = new MediaPlayer();

                    if (!_isCompletionSubscribed)
                    {
                        _isCompletionSubscribed = true;
                        _videoPlayer.Completion += Player_Completion;
                    }

                    _videoPlayer.VideoSizeChanged += (sender, args) =>
                    {
                        AdjustTextureViewAspect(args.Width, args.Height);
                    };

                    _videoPlayer.Info += (sender, args) =>
                    {
                        if (args.What == MediaInfo.VideoRenderingStart)
                        {
                            _placeholder.Visibility = ViewStates.Gone;
                        }
                    };

                    _videoPlayer.Prepared += (sender, args) =>
                    {
                        _mainVideoView.Visibility = ViewStates.Visible;
                        _videoPlayer.Start();
                        if (Element != null)
                            _videoPlayer.Looping = Element.Loop;
                    };
                }

                return _videoPlayer;
            }
        }

        private void Player_Completion(object sender, EventArgs e)
        {
            Element?.OnFinishedPlaying?.Invoke();
        }

        private void PlayVideo(string fullPath)
        {
            Android.Content.Res.AssetFileDescriptor afd = null;

            try
            {
                afd = Context.Assets.OpenFd(fullPath);
            }
            catch (Java.IO.IOException ex)
            {
                _mainVideoView.Visibility = ViewStates.Gone;
            }
            catch (Exception ex)
            {
                _mainVideoView.Visibility = ViewStates.Gone;
            }

            if (afd != null)
            {
                VideoPlayer.Reset();
                VideoPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                VideoPlayer.PrepareAsync();
            }
        }

        private void AdjustTextureViewAspect(int videoWidth, int videoHeight)
        {
            if (!(_mainVideoView is TextureView))
                return;

            if (Control == null)
                return;

            var control = Control;

            var textureView = _mainVideoView as TextureView;

            var controlWidth = control.Width;
            var controlHeight = control.Height;
            var aspectRatio = (double)videoHeight / videoWidth;

            int newWidth, newHeight;

            if (controlHeight <= (int)(controlWidth * aspectRatio))
            {
                // limited by narrow width; restrict height
                newWidth = controlWidth;
                newHeight = (int)(controlWidth * aspectRatio);
            }
            else
            {
                // limited by short height; restrict width
                newWidth = (int)(controlHeight / aspectRatio);
                newHeight = controlHeight;
            }

            int xoff = (controlWidth - newWidth) / 2;
            int yoff = (controlHeight - newHeight) / 2;

            var txform = new Matrix();
            textureView.GetTransform(txform);
            txform.SetScale((float)newWidth / controlWidth, (float)newHeight / controlHeight);
            txform.PostTranslate(xoff, yoff);
            textureView.SetTransform(txform);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Video> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _mainFrameLayout = new FrameLayout(Context);

                _placeholder = new Android.Views.View(Context)
                {
                    Background = new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()),
                    LayoutParameters = new LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent),
                };

                if (Build.VERSION.SdkInt < BuildVersionCodes.IceCreamSandwich)
                {

                    var videoView = new VideoView(Context)
                    {
                        Background = new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()),
                        Visibility = ViewStates.Gone,
                        LayoutParameters = new LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent),
                    };

                    ISurfaceHolder holder = videoView.Holder;
                    if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb)
                    {
                        holder.SetType(SurfaceType.PushBuffers);
                    }
                    holder.AddCallback(this);

                    _mainVideoView = videoView;
                }
                else
                {

                    var textureView = new TextureView(Context)
                    {
                        //Background = new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()),
                        Visibility = ViewStates.Gone,
                        LayoutParameters = new LayoutParams(
                        ViewGroup.LayoutParams.MatchParent,
                        ViewGroup.LayoutParams.MatchParent),
                    };

                    textureView.SurfaceTextureListener = this;

                    _mainVideoView = textureView;
                }

                _mainFrameLayout.AddView(_mainVideoView);
                _mainFrameLayout.AddView(_placeholder);

                SetNativeControl(_mainFrameLayout);

                PlayVideo(Element.Source);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                if (_videoPlayer != null && _isCompletionSubscribed)
                {
                    _isCompletionSubscribed = false;
                    _videoPlayer.Completion -= Player_Completion;
                }
            }
            if (e.NewElement != null)
            {
                // Subscribe
                if (_videoPlayer != null && !_isCompletionSubscribed)
                {
                    _isCompletionSubscribed = true;
                    _videoPlayer.Completion += Player_Completion;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null || Control == null)
                return;

            if (e.PropertyName == Video.SourceProperty.PropertyName)
            {
                Console.WriteLine("Play video: " + Element.Source);
                PlayVideo(Element.Source);
            }
            else if (e.PropertyName == Video.LoopProperty.PropertyName)
            {
                Console.WriteLine("Is Looping? " + Element.Loop);
                VideoPlayer.Looping = Element.Loop;
            }
        }

        private void RemoveVideo()
        {
            _placeholder.Visibility = ViewStates.Visible;
        }

        #region Surface Texture Listener

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            VideoPlayer.SetSurface(new Surface(surface));
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            RemoveVideo();
            return false;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            if (width > height)
            {
                AdjustTextureViewAspect(width, height);
            }
            else
            {
                AdjustTextureViewAspect(height, width);
            }
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }

        #endregion

        #region Surface Holder Callback

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            VideoPlayer.SetDisplay(holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            RemoveVideo();
        }

        #endregion
    }
}
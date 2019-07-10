using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Capture.Frames;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ControlPlaygroundUwpLib
{
    public sealed partial class PlaygroundPage
    {
        private CameraHelper _cameraHelper;
        private MediaPlayer _mediaPlayer;
        private VideoFrame _currentVideoFrame;
        private SoftwareBitmapSource _previewImageSource;
        private SoftwareBitmap _softwareBitmap;

        public PlaygroundPage()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            _previewImageSource = new SoftwareBitmapSource();
            PreviewImage.Source = _previewImageSource;
            _cameraHelper = new CameraHelper();

            IReadOnlyList<MediaFrameSourceGroup> frameSourceGroups = await CameraHelper.GetFrameSourceGroupsAsync();
            CameraHelperResult result = await _cameraHelper.InitializeAndStartCaptureAsync();
            if (result == CameraHelperResult.Success)
            {
                // Subscribe to the video frame as they arrive
                _cameraHelper.FrameArrived += CameraHelper_FrameArrived;
                FrameSourceGroupCombo.ItemsSource = frameSourceGroups;
                FrameSourceGroupCombo.SelectionChanged += FrameSourceGroupCombo_SelectionChanged;
                //FrameSourceGroupCombo.SelectedIndex = 0;

                MediaFrameSource frameSource = _cameraHelper.PreviewFrameSource;
                _mediaPlayer = new MediaPlayer { AutoPlay = true, RealTimePlayback = true };
                _mediaPlayer.Source = MediaSource.CreateFromMediaFrameSource(frameSource);
                MediaPlayerElementControl.SetMediaPlayer(_mediaPlayer);
            }
        }

        private async void FrameSourceGroupCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FrameSourceGroupCombo.SelectedItem is MediaFrameSourceGroup selectedGroup)
            {
                _cameraHelper.FrameSourceGroup = selectedGroup;
                CameraHelperResult result = await _cameraHelper.InitializeAndStartCaptureAsync();

                MediaFrameSource frameSource = _cameraHelper.PreviewFrameSource;
                //_mediaPlayer = new MediaPlayer { AutoPlay = true, RealTimePlayback = true };
                _mediaPlayer.Source = MediaSource.CreateFromMediaFrameSource(frameSource);
                MediaPlayerElementControl.SetMediaPlayer(_mediaPlayer);
            }
        }

        private void CameraHelper_FrameArrived(object sender, FrameEventArgs e)
        {
            _currentVideoFrame = e.VideoFrame;
        }

        private async void TheCaptureButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            SoftwareBitmap softwareBitmap = _currentVideoFrame.SoftwareBitmap;
            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 || softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

            _softwareBitmap = softwareBitmap;

            await _previewImageSource.SetBitmapAsync(_softwareBitmap);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Editing;
using Windows.Media.Effects;
using Windows.Storage.Pickers;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;    

        public bool MediaLoaded { get; set; }
        private MediaClip m_clip;
        private MediaComposition m_composition;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;            
        }

        private void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".avi");

            // Launch file open picker and caller app is suspended and may be terminated if required
            openPicker.PickSingleFileAndContinue();
        }

        /// <summary>
        /// Handle the returned files from file picker
        /// This method is triggered by ContinuationManager based on ActivationKind
        /// </summary>
        /// <param name="args">File open picker continuation activation argment. It cantains the list of files user selected with file open picker </param>
        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                Debug.WriteLine("Picked video: " + args.Files[0].Name);
                MediaLoaded = true;

                m_clip = await MediaClip.CreateFromFileAsync(args.Files[0]);
                m_composition = new MediaComposition();
                m_composition.Clips.Add(m_clip);

                // Update the sliders to represent the duration in milliseconds
                // TODO: move this databinding
                TrimStart.Maximum = m_clip.OriginalDuration.TotalMilliseconds;
                TrimEnd.Maximum = m_clip.OriginalDuration.TotalMilliseconds;
                TrimEnd.Value = TrimEnd.Maximum;

                //// Set up the MediaElement for preview
                // TODO: pass in the preview streamsource and grab the screensize to determine this in addition to the aspect ratio of the video
                MediaElement.SetMediaStreamSource(m_composition.GenerateMediaStreamSource());
            }
            else
            {
                Debug.WriteLine("Operation cancelled.");
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void m_bottomAppBar_Opened(object sender, object e)
        {

        }

        private void m_bottomAppBar_Closed(object sender, object e)
        {

        }

        private void TrimStart_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }

        private void TrimEnd_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }
    }
}

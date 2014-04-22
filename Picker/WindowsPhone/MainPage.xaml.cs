using System;
using System.Diagnostics;
using System.IO;
using Windows.ApplicationModel.Activation;
using Windows.Media.Editing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace Helios
{
    /// <summary>
    /// Implement IFileOpenPickerContinuable interface, in order that Continuation Manager can automatically
    /// trigger the method to process returned files.
    /// </summary>
    public sealed partial class MainPage : Page, IFileOpenPickerContinuable
    {
        public static MainPage Current;

        public bool MediaLoaded { get; set; }
        private MediaClip m_clip;
        private MediaComposition m_composition;
        private string fileName;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;

            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;
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
                fileName = Path.GetFileNameWithoutExtension(args.Files[0].Name);
                Debug.WriteLine("Picked video: " + fileName + " with full name: " + args.Files[0].Name);
                MediaLoaded = true;

                m_clip = await MediaClip.CreateFromFileAsync(args.Files[0]);
                m_composition = new MediaComposition();
                m_composition.Clips.Add(m_clip);

                // Update the sliders to represent the duration in milliseconds
                // TODO: move this databinding
                TrimStart.IsEnabled = true;
                TrimEnd.IsEnabled = true;
                TrimStart.Maximum = m_clip.OriginalDuration.TotalMilliseconds;
                TrimEnd.Maximum = m_clip.OriginalDuration.TotalMilliseconds;
                TrimEnd.Value = TrimStart.Maximum;

                //// Set up the MediaElement for preview
                // TODO: pass in the preview streamsource and grab the screensize to determine this in addition to the aspect ratio of the video
                m_player.SetMediaStreamSource(m_composition.GenerateMediaStreamSource());
            }
            else
            {
                Debug.WriteLine("Operation cancelled.");
            }
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

        private async void TranscodeButton_Click(object sender, RoutedEventArgs e)
        {
	        bool succeeded = false;
	        StatusBar statusBar = StatusBar.GetForCurrentView();
	
	        // Transcoding cannot be used if there is a MediaElement playing; unset it
	        m_player.Source = null;
	
	        // Create a StorageFile to hold the result
            StorageFile outputFile = await KnownFolders.SavedPictures.CreateFileAsync(fileName + "_Trim.mp4", CreationCollisionOption.GenerateUniqueName);

	        try
	        {
		        // Set up the progress bar
		        statusBar.ProgressIndicator.ProgressValue = 0.0f;
		        await statusBar.ProgressIndicator.ShowAsync();
		
		        // Begin rendering
		        var renderOperation = m_composition.RenderToFileAsync(outputFile);
		
		        renderOperation.Progress = async(_, progress) =>
		        {
			        // Update the progress bar
			        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, 
                        () =>
			        {
				        statusBar.ProgressIndicator.ProgressValue = progress / 100.0;
			        });
		        };
		
		        await renderOperation;
		        succeeded = true;
	        }
	        catch(Exception ex)
	        {
		        ShowErrorMessage(ex.Message);
	        }
	
	        await statusBar.ProgressIndicator.HideAsync();
	
	        // Transcode completed, show result
	        if (succeeded)
	        {
		        ContentDialog complete = new ContentDialog();
		        complete.Content = "Transcode complete.";
		        complete.PrimaryButtonText = "Play";
		        complete.SecondaryButtonText = "Cancel";
		        var result = await complete.ShowAsync();
		
		        if (result == ContentDialogResult.Primary)
		        {
			        Uri savedUri = new Uri(outputFile.Path);
			        //Frame.Navigate(typeof(Preview), savedUri);
		        }
	        }
	
	        // Reinitialize the MediaElement now that we are done
	        // TODO: use low res here too
	        var mss = m_composition.GenerateMediaStreamSource();
	        m_player.SetMediaStreamSource(mss);
        }

        private void ShowErrorMessage(string p)
        {
            MessageDialog msgDialog = new MessageDialog(p);
        }

        private void TrimStart_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // TODO: use binding with converters instead of translating the values in code
            // Silverlight multibinding trick for Value and single binding for maximum
            m_clip.TrimTimeFromStart = TimeSpan.FromMilliseconds(TrimStart.Value);
            // Seek to 0:00 to show the new start frame
            m_player.Position = TimeSpan.Zero;
        }

        private void TrimEnd_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            // TODO: databind instead
            m_clip.TrimTimeFromEnd = m_clip.OriginalDuration - TimeSpan.FromMilliseconds(TrimEnd.Value);
            // Seek to the end to show the new end frame
            m_player.Position = m_composition.Duration;
        }
    }
}

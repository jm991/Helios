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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PickAFileButton.Click += new RoutedEventHandler(PickAFileButton_Click);
        }

        private void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFile source;

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
        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files.Count > 0)
            {
                Debug.Log("Picked video: " + args.Files[0].Name);
            }
            else
            {
                Debug.Log(OutputTextBlock.Text = "Operation cancelled.");
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
    }
}

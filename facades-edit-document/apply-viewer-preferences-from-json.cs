using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfViewerPreferenceApplier
{
    // Represents the JSON configuration for viewer preferences.
    public class ViewerSettings
    {
        public bool HideMenubar { get; set; }
        public bool HideToolbar { get; set; }
        public bool HideWindowUI { get; set; }
        public bool FitWindow { get; set; }
        public bool CenterWindow { get; set; }
        public bool DisplayDocTitle { get; set; }
        public bool PageModeUseNone { get; set; }
        public bool PageModeUseOutlines { get; set; }
        public bool PageModeUseThumbs { get; set; }
        public bool PageModeFullScreen { get; set; }
        public bool PageLayoutSinglePage { get; set; }
        public bool PageLayoutOneColumn { get; set; }
        public bool PageLayoutTwoColumnLeft { get; set; }
        public bool PageLayoutTwoColumnRight { get; set; }
        // Add other flags as needed.
    }

    class Program
    {
        static void Main()
        {
            const string configPath = "viewerPreferences.json";   // JSON config file path
            const string inputFolder = "InputPdfs";              // Folder containing source PDFs
            const string outputFolder = "OutputPdfs";            // Folder for processed PDFs

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load and deserialize the JSON configuration.
            ViewerSettings settings;
            try
            {
                string json = File.ReadAllText(configPath);
                settings = JsonSerializer.Deserialize<ViewerSettings>(json);
                if (settings == null)
                {
                    Console.Error.WriteLine("Failed to deserialize configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Build the combined viewer preference flags.
            int viewerPreference = 0;
            if (settings.HideMenubar)          viewerPreference |= ViewerPreference.HideMenubar;
            if (settings.HideToolbar)          viewerPreference |= ViewerPreference.HideToolbar;
            if (settings.HideWindowUI)         viewerPreference |= ViewerPreference.HideWindowUI;
            if (settings.FitWindow)            viewerPreference |= ViewerPreference.FitWindow;
            if (settings.CenterWindow)         viewerPreference |= ViewerPreference.CenterWindow;
            if (settings.DisplayDocTitle)      viewerPreference |= ViewerPreference.DisplayDocTitle;
            if (settings.PageModeUseNone)      viewerPreference |= ViewerPreference.PageModeUseNone;
            if (settings.PageModeUseOutlines)  viewerPreference |= ViewerPreference.PageModeUseOutlines;
            if (settings.PageModeUseThumbs)    viewerPreference |= ViewerPreference.PageModeUseThumbs;
            if (settings.PageModeFullScreen)   viewerPreference |= ViewerPreference.PageModeFullScreen;
            if (settings.PageLayoutSinglePage) viewerPreference |= ViewerPreference.PageLayoutSinglePage;
            if (settings.PageLayoutOneColumn)  viewerPreference |= ViewerPreference.PageLayoutOneColumn;
            if (settings.PageLayoutTwoColumnLeft) viewerPreference |= ViewerPreference.PageLayoutTwoColumnLeft;
            if (settings.PageLayoutTwoColumnRight) viewerPreference |= ViewerPreference.PageLayoutTwoColumnRight;
            // Extend with additional flags as required.

            // Ensure output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Process each PDF file in the input folder.
            foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                try
                {
                    // Use PdfContentEditor to modify viewer preferences.
                    PdfContentEditor editor = new PdfContentEditor();
                    editor.BindPdf(inputPath);
                    editor.ChangeViewerPreference(viewerPreference);
                    editor.Save(outputPath);
                    Console.WriteLine($"Processed: {fileName}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
                }
            }

            Console.WriteLine("All files processed.");
        }
    }
}
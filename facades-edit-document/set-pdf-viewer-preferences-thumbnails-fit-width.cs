using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure an input PDF exists – create a simple one if it does not.
        // This makes the sample self‑contained and prevents FileNotFoundException.
        // ---------------------------------------------------------------------
        if (!System.IO.File.Exists(inputPath))
        {
            using (Document tmpDoc = new Document())
            {
                // Add a blank page (or you could add any content you like).
                tmpDoc.Pages.Add();
                tmpDoc.Save(inputPath);
            }
        }

        // ---------------------------------------------------------------------
        // 1️⃣ Set viewer preferences: show the thumbnails pane and resize the
        //    window so the first page fits the width of the viewer.
        // ---------------------------------------------------------------------
        using (PdfContentEditor viewerPrefEditor = new PdfContentEditor())
        {
            viewerPrefEditor.BindPdf(inputPath);
            int preferences = ViewerPreference.NonFullScreenPageModeUseThumbs |
                               ViewerPreference.FitWindow; // FitWindow makes the window fit the first page width.
            viewerPrefEditor.ChangeViewerPreference(preferences);
            viewerPrefEditor.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 2️⃣ Adjust the default zoom factor.  Setting Zoom to 1.0 (100 %) lets the
        //    viewer honour the FitWindow flag – the page will be displayed at the
        //    maximum width that fits the window.
        // ---------------------------------------------------------------------
        using (PdfPageEditor zoomEditor = new PdfPageEditor())
        {
            zoomEditor.BindPdf(outputPath);
            zoomEditor.Zoom = 1.0f; // 100 % zoom – the viewer will automatically fit the width.
            zoomEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with thumbnails pane and fit‑width zoom: {outputPath}");
    }
}

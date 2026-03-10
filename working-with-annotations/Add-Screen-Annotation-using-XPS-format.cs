using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // for XpsSaveOptions (same namespace as Document)

class Program
{
    static void Main()
    {
        // Input PDF (can be empty or existing). Here we create a new PDF.
        const string outputPdfPath  = "output_with_screen.pdf";
        const string outputXpsPath  = "output_with_screen.xps";
        const string mediaFilePath  = "sample_video.mp4"; // path to the media file to be played

        // Verify that the media file exists
        if (!File.Exists(mediaFilePath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFilePath}");
            return;
        }

        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the screen annotation will appear
            // Coordinates are in points (1/72 inch), origin is bottom‑left of the page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation – it references the media file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaFilePath)
            {
                Title = "Sample Video",
                Contents = "Click to play the video."
                // Additional properties (Color, Border, etc.) can be set here if needed
            };

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the PDF (optional, shows the PDF result)
            doc.Save(outputPdfPath);

            // Save the document as XPS – must use XpsSaveOptions explicitly
            XpsSaveOptions xpsOpts = new XpsSaveOptions();
            doc.Save(outputXpsPath, xpsOpts);
        }

        Console.WriteLine($"PDF with Screen annotation saved to '{outputPdfPath}'.");
        Console.WriteLine($"XPS version saved to '{outputXpsPath}'.");
    }
}
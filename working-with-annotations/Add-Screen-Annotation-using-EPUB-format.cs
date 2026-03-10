using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the media file to be played, and the output EPUB.
        const string inputPdf   = "input.pdf";
        const string mediaFile  = "sample.mp4";
        const string outputEpub = "output.epub";

        // Verify that required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle where the screen annotation will appear.
            // Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a ScreenAnnotation that references the media file.
            // Constructor: ScreenAnnotation(Page, Rectangle, string mediaFile)
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaFile)
            {
                Title    = "Sample Video",          // Title shown in the annotation UI.
                Contents = "Click to play the video" // Tooltip / popup text.
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(screenAnn);

            // Prepare EPUB save options – must be passed explicitly (otherwise PDF is saved).
            EpubSaveOptions epubOpts = new EpubSaveOptions
            {
                Title = "PDF with Screen Annotation"
                // ContentRecognitionMode defaults to Flow; can be set if desired.
            };

            // Save the modified document as EPUB using the explicit options.
            doc.Save(outputEpub, epubOpts);
        }

        Console.WriteLine($"EPUB saved to '{outputEpub}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xslFoPath   = "input.xslfo";      // XSL‑FO source file
        const string mediaPath   = "sample.mp4";      // Media file for the screen annotation
        const string outputPdf   = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        try
        {
            // Load the XSL‑FO document into a PDF
            using (Document pdfDoc = new Document(xslFoPath, new XslFoLoadOptions()))
            {
                // Choose the page where the annotation will be placed (first page in this example)
                Page page = pdfDoc.Pages[1];

                // Define the rectangle for the annotation (left, bottom, right, top)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

                // Create the screen annotation with the media file path
                ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, mediaPath)
                {
                    // Optional: set a title and contents for the annotation
                    Title    = "Sample Video",
                    Contents = "Click to play the video."
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(screenAnn);

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with screen annotation saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string htmlOutput = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNGs inside SVG to keep the output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML; wrap in try‑catch for GDI+ (Windows‑only) limitation
                try
                {
                    doc.Save(htmlOutput, htmlOptions);
                    Console.WriteLine("Split to HTML pages.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
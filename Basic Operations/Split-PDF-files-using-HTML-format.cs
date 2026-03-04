using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string pdfPath = "input.pdf";

        // Base name for the generated HTML files.
        // When SplitIntoPages is true, Aspose.Pdf creates:
        //   output.html (first page) and output_2.html, output_3.html, ...
        const string htmlOutput = "output.html";

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion options.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Split each PDF page into a separate HTML file.
                    SplitIntoPages = true,

                    // Embed raster images as PNG inside SVG to avoid external image files.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Perform the conversion. Multiple HTML files will be created.
                doc.Save(htmlOutput, htmlOptions);
                Console.WriteLine("PDF successfully split into HTML pages.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
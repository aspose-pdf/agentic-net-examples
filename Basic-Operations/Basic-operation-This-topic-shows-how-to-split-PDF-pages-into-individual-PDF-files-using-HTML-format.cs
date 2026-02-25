using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string htmlOutput = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion to generate one HTML file per PDF page
                HtmlSaveOptions opts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed images as PNGs inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document; each page will be written to a separate HTML file
                // (output.html, output_1.html, output_2.html, ... depending on the page count)
                doc.Save(htmlOutput, opts);
                Console.WriteLine($"PDF split into HTML pages successfully. Base name: '{htmlOutput}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is only supported on Windows
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
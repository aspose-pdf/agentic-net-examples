using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html"; // Base name; multiple files will be created

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed raster images as PNG inside SVG for better compatibility
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document; Aspose.Pdf will generate output.html, output_page_1.html, etc.
                pdfDocument.Save(outputHtml, htmlOptions);
            }

            Console.WriteLine("PDF successfully split into separate HTML pages.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
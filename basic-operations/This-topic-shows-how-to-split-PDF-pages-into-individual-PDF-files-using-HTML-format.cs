using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Base output HTML file name (Aspose.Pdf will generate one HTML per page)
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed raster images as PNG inside SVG to keep the output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document. Aspose.Pdf will create multiple HTML files
                // (e.g., output.html, output_1.html, output_2.html, …) corresponding to each page.
                pdfDoc.Save(outputHtmlPath, htmlOptions);
                Console.WriteLine("PDF successfully split into individual HTML pages.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ which is only available on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
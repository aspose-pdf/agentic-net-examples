using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
// No additional using directives for Aspose.Pdf.Saving – all SaveOptions are in Aspose.Pdf

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";
        // Output HTML file path (will contain only the <body> markup)
        const string outputHtmlPath = "output_body.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Generate only the content inside the <body> element
                    HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent,
                    // Optional: embed images as PNGs wrapped in SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the PDF as HTML using the configured options
                pdfDoc.Save(outputHtmlPath, htmlOpts);
            }

            Console.WriteLine($"HTML body content saved to '{outputHtmlPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
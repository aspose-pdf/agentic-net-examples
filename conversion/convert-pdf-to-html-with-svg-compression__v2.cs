using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputHtmlPath = "output.html";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize HtmlSaveOptions and enable SVG compression
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    CompressSvgGraphicsIfAny = true
                };

                // Save as HTML using the options (explicit SaveOptions required)
                pdfDoc.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"HTML file saved to '{outputHtmlPath}'.");
        }
        // HTML conversion relies on GDI+ and may fail on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
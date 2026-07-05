using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Compress any SVG graphics found during conversion
                    CompressSvgGraphicsIfAny = true
                };

                // Save as HTML with the specified options
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: '{outputHtmlPath}'");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ and is only supported on Windows
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
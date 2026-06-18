using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Configure HTML save options with SVG compression
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();
                htmlOpts.CompressSvgGraphicsIfAny = true;

                // Save as HTML
                doc.Save(outputPath, htmlOpts);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ (Windows only)
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
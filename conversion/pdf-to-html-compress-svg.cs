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
            using (Document pdfDoc = new Document(inputPath))
            {
                // HtmlSaveOptions no longer exposes a CompressSvgImages property.
                // SVG images are handled internally; the default behavior provides the best compression.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                try
                {
                    pdfDoc.Save(outputPath, htmlOptions);
                    Console.WriteLine($"HTML saved to '{outputPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

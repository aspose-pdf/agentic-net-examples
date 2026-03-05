using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create & load)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure HTML save options for single‑page output
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Ensure the result is a single HTML file (default is false, set explicitly)
                SplitIntoPages = false
            };

            // Save the document as HTML (lifecycle: save)
            pdfDoc.Save(outputPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to HTML: {outputPath}");
    }
}
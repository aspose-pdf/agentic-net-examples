using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (var doc = new Document(inputPath))
            {
                // Prepare an array with all page numbers (1‑based)
                int[] pages = Enumerable.Range(1, doc.Pages.Count).ToArray();

                // Create resize parameters with a 10% margin on each side (default behavior)
                var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(0.1, 0.1, 0.1, 0.1);

                // Resize the contents of the selected pages
                new PdfFileEditor().ResizeContents(doc, pages, resizeParams);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

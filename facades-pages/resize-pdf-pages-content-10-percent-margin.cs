using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Prepare an array with all page numbers.
            int[] pages = Enumerable.Range(1, doc.Pages.Count).ToArray();

            // Calculate a 10% margin based on the first page width (same margin for all sides).
            double margin = doc.Pages[1].PageInfo.Width * 0.10;

            // Create resize parameters with the calculated margin.
            var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(margin, margin, margin, margin);

            // Perform the resize operation.
            new PdfFileEditor().ResizeContents(doc, pages, resizeParams);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
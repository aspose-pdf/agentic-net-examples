using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the first page (or any other page index)
            Page page = doc.Pages[1];

            // Calculate a new MediaBox rectangle.
            // Example: inset 50 points from each side of the original MediaBox.
            double left   = page.MediaBox.LLX + 50;
            double bottom = page.MediaBox.LLY + 50;
            double right  = page.MediaBox.URX - 50;
            double top    = page.MediaBox.URY - 50;

            // Set the MediaBox to the new rectangle.
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity.
            page.MediaBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page firstPage = doc.Pages[1];

            // Set the page size to 500 × 700 points.
            // MediaBox defines the visible page area; using a fully qualified Rectangle avoids ambiguity.
            firstPage.MediaBox = new Aspose.Pdf.Rectangle(0, 0, 500, 700);

            // Optionally also update CropBox to match the new size
            firstPage.CropBox = new Aspose.Pdf.Rectangle(0, 0, 500, 700);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"First page resized and saved to '{outputPath}'.");
    }
}
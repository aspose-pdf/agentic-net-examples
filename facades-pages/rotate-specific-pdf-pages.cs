using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Pages to rotate (1‑based indexes)
        int[] pagesToRotate = { 3, 5, 7 };
        foreach (int pageNumber in pagesToRotate)
        {
            // Ensure the page exists
            if (pageNumber <= pdfDocument.Pages.Count)
            {
                pdfDocument.Pages[pageNumber].Rotate = Rotation.on180; // 180° rotation
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Pages 3, 5, and 7 rotated 180° and saved to '{outputPath}'.");
    }
}

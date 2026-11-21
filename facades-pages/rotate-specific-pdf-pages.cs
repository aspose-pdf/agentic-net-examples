using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Define the pages to rotate (1‑based indexing)
        int[] pagesToRotate = { 3, 5, 7 };

        // Apply a 180° rotation to each specified page
        foreach (int pageNumber in pagesToRotate)
        {
            // Ensure the page exists in the document
            if (pageNumber <= pdfDoc.Pages.Count && pageNumber > 0)
            {
                pdfDoc.Pages[pageNumber].Rotate = Rotation.on180;
            }
            else
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
            }
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Pages 3, 5, and 7 have been rotated 180° and saved to '{outputPath}'.");
    }
}

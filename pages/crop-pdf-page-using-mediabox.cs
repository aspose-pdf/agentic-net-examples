using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the page exists (pages are 1‑based)
            int pageNumber = 1;
            if (pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Requested page number exceeds page count.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Define a new MediaBox (left, bottom, right, top)
            // Example: a 400×600 region starting at (50,50)
            Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(50, 50, 450, 650);

            // Set the MediaBox to the custom region
            page.MediaBox = newBox;

            // Optionally set the CropBox to the same region to enforce clipping
            page.CropBox = newBox;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
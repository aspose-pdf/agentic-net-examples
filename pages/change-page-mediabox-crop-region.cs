using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define a new MediaBox rectangle (llx, lly, urx, ury)
            // Example: a 400×600 region starting at (50, 50)
            Aspose.Pdf.Rectangle newMediaBox = new Aspose.Pdf.Rectangle(50, 50, 450, 650);

            // Set the MediaBox to change the page size / visible region
            page.MediaBox = newMediaBox;

            // Optionally set the CropBox to the same rectangle to enforce clipping
            page.CropBox = newMediaBox;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
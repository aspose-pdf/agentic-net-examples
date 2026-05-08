using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "cropped.pdf";

        // Page number to modify (Aspose.Pdf uses 1‑based indexing)
        const int pageNumber = 1;

        // Desired MediaBox coordinates (lower‑left x/y and upper‑right x/y)
        double llx = 50;   // left
        double lly = 50;   // bottom
        double urx = 500;  // right
        double ury = 700;  // top

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}");
                return;
            }

            // Set a custom MediaBox – this defines the visible page area (cropping region)
            // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            doc.Pages[pageNumber].MediaBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
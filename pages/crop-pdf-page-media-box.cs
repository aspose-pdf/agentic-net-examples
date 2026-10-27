using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Choose the page to modify (1‑based indexing)
            int pageNumber = 1;
            Page page = doc.Pages[pageNumber];

            // Calculate a new MediaBox that crops 50 points from each side
            double llx = page.MediaBox.LLX + 50; // lower‑left X
            double lly = page.MediaBox.LLY + 50; // lower‑left Y
            double urx = page.MediaBox.URX - 50; // upper‑right X
            double ury = page.MediaBox.URY - 50; // upper‑right Y

            // Assign the new MediaBox using a fully qualified Rectangle type
            page.MediaBox = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Save the modified PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
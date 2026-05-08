using System;
using System.IO;
using Aspose.Pdf; // Document, Page, Rectangle are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cropped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Original media box of the page
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                // Define how much to trim from each side (e.g., 10 units)
                double inset = 10.0;

                // Create a new CropBox rectangle inset from the media box
                Aspose.Pdf.Rectangle cropBox = new Aspose.Pdf.Rectangle(
                    mediaBox.LLX + inset, // left
                    mediaBox.LLY + inset, // bottom
                    mediaBox.URX - inset, // right
                    mediaBox.URY - inset  // top
                );

                // Apply the CropBox to remove margins
                page.CropBox = cropBox;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
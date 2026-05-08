using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bleed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Process each page (example uses first page; adjust as needed)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the existing CropBox
                Aspose.Pdf.Rectangle crop = page.CropBox;

                // Extend 5 points on each side to create the BleedBox
                Aspose.Pdf.Rectangle bleed = new Aspose.Pdf.Rectangle(
                    crop.LLX - 5,   // left
                    crop.LLY - 5,   // bottom
                    crop.URX + 5,   // right
                    crop.URY + 5    // top
                );

                // Assign the new BleedBox
                page.BleedBox = bleed;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated BleedBox: {outputPath}");
    }
}
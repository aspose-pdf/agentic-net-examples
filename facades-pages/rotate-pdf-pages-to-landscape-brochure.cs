using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "brochure_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Standard brochure size: A4 landscape (842 x 595 points).
            const double brochureWidth = 842;  // points (11.69 inches)
            const double brochureHeight = 595; // points (8.27 inches)

            // Rotate each page 90 degrees clockwise and set the page size.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Rotate the page to landscape orientation.
                doc.Pages[i].Rotate = Rotation.on90; // 90° clockwise

                // Adjust the page dimensions to the brochure size.
                doc.Pages[i].PageInfo.Width = brochureWidth;
                doc.Pages[i].PageInfo.Height = brochureHeight;
                // Optional: mark the page as landscape for clarity.
                doc.Pages[i].PageInfo.IsLandscape = brochureWidth > brochureHeight;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Brochure PDF saved to '{outputPath}'.");
    }
}

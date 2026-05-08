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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define corporate brand color (RGB values)
            int r = 30;   // Red component (0‑255)
            int g = 144;  // Green component (0‑255)
            int b = 255;  // Blue component (0‑255)

            // Create Aspose.Pdf.Color from RGB components
            Aspose.Pdf.Color brandColor = Aspose.Pdf.Color.FromArgb(r, g, b);

            // Apply the background color to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = brandColor;
            }

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page background color to '{outputPath}'.");
    }
}
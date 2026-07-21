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

        // Load the PDF, modify, and save – wrapped in a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the corporate brand color (example RGB 30,144,255)
            Aspose.Pdf.Color brandColor = Aspose.Pdf.Color.FromArgb(30, 144, 255);

            // Apply the background color to each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = brandColor;
            }

            // Optionally set the document‑wide background as well
            doc.Background = brandColor;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with page background color to '{outputPath}'.");
    }
}
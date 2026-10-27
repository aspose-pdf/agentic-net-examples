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

        // Open the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Define a CMYK color (values are in the range 0.0 – 1.0)
            // Example: 0% cyan, 100% magenta, 0% yellow, 0% black
            Aspose.Pdf.Color cmykColor = Aspose.Pdf.Color.FromCmyk(0.0, 1.0, 0.0, 0.0);

            // Apply the background color to each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Aspose.Pdf.Page page = doc.Pages[i];
                page.Background = cmykColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background color to '{outputPath}'.");
    }
}
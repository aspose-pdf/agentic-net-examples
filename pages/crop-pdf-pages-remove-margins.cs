using System;
using System.IO;
using Aspose.Pdf;

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

        // Margin to trim from each side (in points). Adjust as needed.
        const double margin = 50.0;

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Calculate new CropBox coordinates based on page size and desired margin.
                double left   = margin;
                double bottom = margin;
                double right  = page.PageInfo.Width  - margin;
                double top    = page.PageInfo.Height - margin;

                // Set the CropBox using a fully qualified Rectangle to avoid ambiguity.
                page.CropBox = new Aspose.Pdf.Rectangle(left, bottom, right, top);
            }

            // Save the modified document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
    }
}
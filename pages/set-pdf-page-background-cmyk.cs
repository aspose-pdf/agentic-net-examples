using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // CMYK components (range 0.0 – 1.0) for the desired background color
            double cyan    = 0.0;   // example: no cyan
            double magenta = 0.5;   // 50% magenta
            double yellow  = 0.5;   // 50% yellow
            double black   = 0.0;   // no black

            // Apply the background color to each page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = Color.FromCmyk(cyan, magenta, yellow, black);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For Color class (if needed, but Color is also in Aspose.Pdf)

class Program
{
    static void Main()
    {
        // Input PDF path – replace with your actual file
        const string inputPath  = "input.pdf";
        // Output PDF path – the file with the new background color
        const string outputPath = "output_with_cmyk_background.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // CMYK values (range 0.0 – 1.0). Example: 30% cyan, 20% magenta, 0% yellow, 10% black
            double cyan    = 0.30;
            double magenta = 0.20;
            double yellow  = 0.00;
            double black   = 0.10;

            // Create a Color object from CMYK components
            Color cmykColor = Color.FromCmyk(cyan, magenta, yellow, black);

            // Apply the background color to each page (or a specific page)
            foreach (Page page in doc.Pages)
            {
                page.Background = cmykColor;
            }

            // Save the modified PDF. No SaveOptions needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}
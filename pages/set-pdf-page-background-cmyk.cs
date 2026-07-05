using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For Color utilities

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cmyk_background.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define CMYK components (range 0.0 – 1.0)
            double cyan    = 0.0;   // 0% cyan
            double magenta = 0.5;   // 50% magenta
            double yellow  = 0.5;   // 50% yellow
            double black   = 0.0;   // 0% black

            // Create a PDF Color from CMYK values
            Aspose.Pdf.Color cmykColor = Aspose.Pdf.Color.FromCmyk(cyan, magenta, yellow, black);

            // Apply the background color to every page
            foreach (Page page in doc.Pages)
            {
                page.Background = cmykColor;
            }

            // Save the modified PDF (lifecycle: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}
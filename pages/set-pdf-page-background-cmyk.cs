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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a CMYK color (Cyan, Magenta, Yellow, Black) with values in the 0‑1 range
            // Example: 100% Cyan, 0% Magenta, 0% Yellow, 0% Black
            Color cmykColor = Color.FromCmyk(1.0, 0.0, 0.0, 0.0);

            // Set the background color for each page via the Page.Background property
            foreach (Page page in doc.Pages)
            {
                page.Background = cmykColor;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CMYK background to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based indexing for pages
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Rotate each page 180 degrees using the correct enum member
                page.Rotate = Rotation.on180; // Aspose.Pdf.Rotation enum
            }

            // Save the modified document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

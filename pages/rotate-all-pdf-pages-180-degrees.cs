using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Text;          // For Rotation enum (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Set rotation to 180 degrees (clockwise)
                page.Rotate = Rotation.on180;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}
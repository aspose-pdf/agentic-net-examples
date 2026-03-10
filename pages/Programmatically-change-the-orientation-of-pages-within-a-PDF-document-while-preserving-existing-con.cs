using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                // Set desired orientation.
                // Example: rotate each page 90 degrees clockwise.
                // Other options: Rotation.None, Rotation.on180, Rotation.on270
                page.Rotate = Rotation.on90;
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages reoriented and saved to '{outputPath}'.");
    }
}

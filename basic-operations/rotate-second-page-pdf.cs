using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; page 2 is the second page
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Rotate the second page 90 degrees clockwise
            // Use the correct enum value with the 'on' prefix
            doc.Pages[2].Rotate = Rotation.on90;

            // Save the modified document (extension .pdf ensures PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

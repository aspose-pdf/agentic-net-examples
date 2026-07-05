using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that a second page exists (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Access the second page
            Page secondPage = doc.Pages[2];

            // Rotate 90 degrees clockwise (use the correct enum value)
            secondPage.Rotate = Rotation.on90;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

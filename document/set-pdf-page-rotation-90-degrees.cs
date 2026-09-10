using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Set initial rotation to 90 degrees (portrait on landscape devices)
            // Rotation enum values use the 'on' prefix (e.g., on90, on180)
            for (int i = 1; i <= doc.Pages.Count; i++) // pages are 1‑based
            {
                doc.Pages[i].Rotate = Rotation.on90;
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

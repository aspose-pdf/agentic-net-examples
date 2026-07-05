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

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Set each page's rotation to 90 degrees (portrait on landscape devices)
            for (int i = 1; i <= doc.Pages.Count; i++) // page indexing is 1‑based
            {
                Page page = doc.Pages[i];
                page.Rotate = Rotation.on90; // Correct enum value with 'on' prefix
            }

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with rotation applied: {outputPath}");
    }
}
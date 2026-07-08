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

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (global rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Set rotation based on page number parity using the correct enum values
                page.Rotate = (i % 2 == 0) ? Rotation.on90 : Rotation.None;
            }

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page to rotate.");
                return;
            }

            // Pages are 1‑based; page 2 is the second page
            Page secondPage = doc.Pages[2];

            // Rotate the page 90 degrees clockwise (use the correct enum value)
            secondPage.Rotate = Rotation.on90;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Second page rotated and saved to '{outputPath}'.");
    }
}

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

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Rotate each page 180 degrees using the correct API
            foreach (Page page in doc.Pages)
            {
                page.Rotate = Rotation.on180; // correct enum member
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages rotated 180° and saved to '{outputPath}'.");
    }
}
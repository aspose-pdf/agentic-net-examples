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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Set each page's rotation to 90 degrees (portrait on landscape devices)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Correct enum values use the "on" prefix
                doc.Pages[i].Rotate = Rotation.on90;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with rotation applied: '{outputPath}'");
    }
}

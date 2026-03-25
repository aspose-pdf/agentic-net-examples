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

        using (Document doc = new Document(inputPath))
        {
            // Verify that a second page exists
            if (doc.Pages.Count >= 2)
            {
                // Rotate the second page 90 degrees clockwise using the correct enum value
                doc.Pages[2].Rotate = Rotation.on90;
            }
            else
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated PDF to '{outputPath}'.");
    }
}
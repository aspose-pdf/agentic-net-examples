using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
            // Rotate pages starting from the last page moving backwards
            for (int i = doc.Pages.Count; i >= 1; i--)
            {
                // Apply a 90‑degree clockwise rotation to each page
                doc.Pages[i].Rotate = Aspose.Pdf.Rotation.on90;
            }

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
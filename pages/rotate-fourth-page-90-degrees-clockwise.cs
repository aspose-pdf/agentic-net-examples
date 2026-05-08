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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count >= 4)
            {
                // Rotate page 4 by 90 degrees clockwise using the Rotate property
                // Rotation.on90 corresponds to a 90‑degree clockwise rotation
                doc.Pages[4].Rotate = Rotation.on90;
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated 90° clockwise and saved to '{outputPath}'.");
    }
}
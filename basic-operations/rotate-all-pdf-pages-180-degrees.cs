using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Pages collection is 1‑based; rotate each page 180 degrees
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Correct enum value uses the "on" prefix
                    doc.Pages[i].Rotate = Rotation.on180;
                }

                // Save the rotated document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

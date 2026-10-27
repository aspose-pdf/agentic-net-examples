using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Rotate every third page (3, 6, 9, ...) by 270 degrees (landscape)
                for (int pageNumber = 3; pageNumber <= doc.Pages.Count; pageNumber += 3)
                {
                    // Aspose.Pdf uses a 1‑based page index and the Rotation enum
                    doc.Pages[pageNumber].Rotate = Rotation.on270;
                }

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Pages rotated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

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

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Rotate each page 90 degrees clockwise
                // Page collection is 1‑based (rule: page-indexing-one-based)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.Rotate = Rotation.on90; // Correct enum value (rule: rotation-enum-use-on-prefix)
                }

                // Save the modified document as PDF (lifecycle rule: use Save)
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

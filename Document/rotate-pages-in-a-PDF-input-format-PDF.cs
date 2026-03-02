using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, Page, Rotation, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document doc = new Document(inputPath))
            {
                // Rotate every page 90 degrees clockwise.
                // Page collection is 1‑based, but foreach abstracts that.
                foreach (Page page in doc.Pages)
                {
                    // Rotation enum values: None, on90, on180, on270, on360
                    page.Rotate = Rotation.on90;
                }

                // Save the modified document (PDF format, no extra SaveOptions needed)
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
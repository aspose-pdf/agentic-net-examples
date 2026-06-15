using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

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

        try
        {
            // Load the PDF document (using rule: document-disposal-with-using)
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least two pages (pages are 1‑based)
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The document does not contain a second page.");
                    return;
                }

                // Rotate the second page 90 degrees clockwise
                // Rotation enum values use the 'on' prefix (e.g., on90, on180, etc.)
                doc.Pages[2].Rotate = Rotation.on90;

                // Save the modified document (using rule: document-disposal-with-using)
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

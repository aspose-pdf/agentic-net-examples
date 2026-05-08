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
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that the document has at least two pages
                if (doc.Pages.Count < 2)
                {
                    Console.Error.WriteLine("The document does not contain a second page.");
                    return;
                }

                // Rotate the second page 90 degrees clockwise
                doc.Pages[2].Rotate = Rotation.on90; // Correct enum value ("on" prefix)

                // Save the modified document
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

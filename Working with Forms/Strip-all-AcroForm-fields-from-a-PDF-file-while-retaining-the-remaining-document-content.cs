using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document doc = new Document(inputPath))
            {
                // Flatten removes all AcroForm fields and places their values directly on the page.
                doc.Flatten();

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"AcroForm fields stripped. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
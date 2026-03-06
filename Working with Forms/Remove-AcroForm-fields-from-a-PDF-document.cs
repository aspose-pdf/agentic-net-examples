using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Remove all AcroForm fields by flattening them onto the page content
            doc.Form.Flatten();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"AcroForm fields removed. Saved to '{outputPath}'.");
    }
}
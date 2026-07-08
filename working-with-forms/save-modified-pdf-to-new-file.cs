using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the original PDF document
        using (Document doc = new Document(inputPath))
        {
            // ----- Place any modifications to the document here -----
            // Example modification: add an empty page at the end
            doc.Pages.Add();

            // Save the modified document to a new file name,
            // preserving the original file unchanged
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}
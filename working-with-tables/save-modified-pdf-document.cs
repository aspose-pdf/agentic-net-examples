using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // ----- Perform any desired modifications here -----
            // Example: add a blank page at the end of the document
            doc.Pages.Add();

            // Save the edited document in PDF format.
            // Document.Save(string) always writes PDF regardless of the file extension,
            // so no SaveOptions are required for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
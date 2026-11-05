using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ps";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PostScript file with default load options.
            PsLoadOptions loadOptions = new PsLoadOptions();

            // Wrap the Document in a using block for deterministic disposal.
            using (Document doc = new Document(inputPath, loadOptions))
            {
                // Save the document as PDF (default format).
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
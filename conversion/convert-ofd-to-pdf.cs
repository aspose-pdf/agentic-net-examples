using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, OfdLoadOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";
        const string outputPath = "output.pdf";

        // Verify the source OFD file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the OFD file using the appropriate load options
            OfdLoadOptions loadOptions = new OfdLoadOptions();

            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath, loadOptions))
            {
                // Save the loaded document as PDF (default settings)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted OFD to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.ofd";
        const string outputPath = "output.pdf";

        // Verify the source OFD file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the OFD file using the appropriate load options
        OfdLoadOptions loadOptions = new OfdLoadOptions();

        // Document implements IDisposable; wrap in using for deterministic cleanup
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the loaded document as PDF with default settings
            doc.Save(outputPath);
        }

        Console.WriteLine($"OFD successfully converted to PDF: {outputPath}");
    }
}
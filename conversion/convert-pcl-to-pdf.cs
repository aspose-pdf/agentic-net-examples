using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pcl";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load options for PCL: suppress any parsing errors (property not required in current API)
        PclLoadOptions loadOptions = new PclLoadOptions();

        // Load the PCL file, convert it to PDF, and save
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Saving without explicit SaveOptions writes PDF (default format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion complete. PDF saved to '{outputPath}'.");
    }
}

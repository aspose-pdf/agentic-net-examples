using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pcl";
        const string outputPath = "output.pdf";

        // Verify the source PCL file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // HP‑GL/2 vectors are loaded automatically; use default load options
        var loadOptions = new PclLoadOptions();

        // Load the PCL document with the specified options
        using (var doc = new Document(inputPath, loadOptions))
        {
            // Save the document as PDF (default format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
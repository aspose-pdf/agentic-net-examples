using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.cgm";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the CGM file using default load options (A4 page size, 300 DPI)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Document implements IDisposable; use a using block for proper disposal
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the converted document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"CGM file successfully converted to PDF: {outputPath}");
    }
}
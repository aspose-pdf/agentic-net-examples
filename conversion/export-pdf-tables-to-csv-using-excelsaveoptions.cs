using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "DefaultTables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Export tables using ExcelSaveOptions with default settings
            ExcelSaveOptions excelOpts = new ExcelSaveOptions(); // defaults
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Tables exported to CSV: {outputPath}");
    }
}
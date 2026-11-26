using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "data_sheet.pdf";
        const string outputPath = "data_sheet.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to output CSV format
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV; passing SaveOptions ensures non‑PDF output
            doc.Save(outputPath, csvOptions);
        }

        Console.WriteLine($"PDF successfully converted to CSV: {outputPath}");
    }
}
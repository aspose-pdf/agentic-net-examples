using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog_min.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for ODS output
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the output format to ODS
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                // Minimize the number of worksheets in the resulting file
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the document as ODS using the configured options
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
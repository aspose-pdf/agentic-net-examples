using System;
using System.IO;
using Aspose.Pdf;                     // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog_blank.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for ODS output
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Ensure the output format is ODS
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                // Insert a blank column at the beginning of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the document as ODS using the explicit options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog_blank.xlsm";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for XLSM output and to insert a blank column at the first position
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the desired Excel format (XLSM)
                Format = ExcelSaveOptions.ExcelFormat.XLSM,
                // Enable insertion of a blank column before the first data column
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an XLSM workbook using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog_blank.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Ensure the output format is XLSX
                Format = ExcelSaveOptions.ExcelFormat.XLSX,
                // Insert a blank column at the beginning of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an Excel workbook using the specified options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
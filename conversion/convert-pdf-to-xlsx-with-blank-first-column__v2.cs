using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "summary.pdf";
        const string outputPath = "summary_blank.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions: set format to XLSX and enable blank column insertion
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSX,
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an Excel workbook using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
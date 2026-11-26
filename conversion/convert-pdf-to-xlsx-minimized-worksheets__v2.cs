using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report_min.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Ensure the output is XLSX format
                Format = ExcelSaveOptions.ExcelFormat.XLSX,
                // Minimize the number of worksheets in the generated workbook
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the PDF as an Excel workbook using the specified options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}
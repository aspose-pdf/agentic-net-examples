using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputExcelPath = "output.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize ExcelSaveOptions (default format is XLSX)
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Explicitly set the format if desired
                // Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook using the options
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputExcelPath}");
    }
}
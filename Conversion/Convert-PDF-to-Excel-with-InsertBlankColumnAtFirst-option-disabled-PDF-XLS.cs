using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output Excel file
        const string inputPdfPath = "input.pdf";
        const string outputExcelPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize ExcelSaveOptions and explicitly disable the blank column insertion
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                InsertBlankColumnAtFirst = false // default is false, set explicitly per requirement
            };

            // Save the PDF as an Excel workbook using the options
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"Conversion completed. Excel file saved to '{outputExcelPath}'.");
    }
}
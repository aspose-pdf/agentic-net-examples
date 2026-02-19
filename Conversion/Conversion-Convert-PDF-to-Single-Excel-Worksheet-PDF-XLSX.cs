using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output Excel file
        string inputPdfPath = "input.pdf";
        string outputExcelPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Configure Excel save options:
        // - MinimizeTheNumberOfWorksheets = true merges all pages into a single worksheet.
        // - Format = XLSX ensures the output is an .xlsx file (default value).
        ExcelSaveOptions excelOptions = new ExcelSaveOptions
        {
            MinimizeTheNumberOfWorksheets = true,
            Format = ExcelSaveOptions.ExcelFormat.XLSX
        };

        // Save the PDF as an Excel workbook using the configured options
        pdfDocument.Save(outputExcelPath, excelOptions);

        Console.WriteLine($"Conversion completed. Excel file saved to '{outputExcelPath}'.");
    }
}
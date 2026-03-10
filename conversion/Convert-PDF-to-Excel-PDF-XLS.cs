using System;
using System.IO;
using Aspose.Pdf; // Document and ExcelSaveOptions are in this namespace

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
            // Initialize ExcelSaveOptions (required when saving to a non‑PDF format)
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Optional: minimize the number of worksheets (one worksheet per PDF page by default)
            // excelOptions.MinimizeTheNumberOfWorksheets = true;

            // Save the PDF as an Excel workbook using the options
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputExcelPath}");
    }
}
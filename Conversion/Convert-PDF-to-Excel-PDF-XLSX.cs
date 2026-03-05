using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXls = "output.xlsx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize ExcelSaveOptions (required when saving to a non‑PDF format)
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();
            // The default format is XLSX; you can change it if needed:
            // excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX;

            // Save the PDF as an Excel workbook
            pdfDocument.Save(outputXls, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputXls}");
    }
}
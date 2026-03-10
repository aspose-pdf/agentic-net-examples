using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the target Excel file
        const string pdfPath  = "input.pdf";
        const string xlsPath  = "output.xls";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize ExcelSaveOptions – required when saving to a non‑PDF format
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Optionally, you can control the exact Excel format:
            // excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX; // default is XLSX
            // excelOptions.Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003; // for .xls (XML) format

            // Save the PDF as an Excel workbook
            pdfDocument.Save(xlsPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {xlsPath}");
    }
}
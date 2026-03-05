using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination Excel file.
        const string pdfPath = "input.pdf";
        const string xlsPath = "output.xlsx";

        // Ensure the source file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        // Load the PDF, configure Excel export options, and save as XLS/XLSX.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize ExcelSaveOptions.
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Insert a blank column as the first column (control column).
            excelOptions.InsertBlankColumnAtFirst = true;

            // Save the PDF as an Excel workbook using the configured options.
            pdfDocument.Save(xlsPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {xlsPath}");
    }
}
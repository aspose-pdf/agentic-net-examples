using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions

class PdfToXlsxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output XLSX file path
        const string xlsxPath = "output.xlsx";

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF, convert and save as XLSX using ExcelSaveOptions
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize save options for Excel format (XLSX is default)
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Save the document as an Excel workbook
            pdfDocument.Save(xlsxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSX: '{xlsxPath}'.");
    }
}
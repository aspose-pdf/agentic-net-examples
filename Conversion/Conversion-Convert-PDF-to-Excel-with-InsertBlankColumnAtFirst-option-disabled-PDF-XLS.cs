using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (change as needed)
        string pdfPath = "input.pdf";

        // Output Excel file path (change as needed)
        string excelPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure Excel save options
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Ensure no blank column is inserted as the first column
                InsertBlankColumnAtFirst = false
            };

            // Save the PDF as an Excel workbook using the configured options
            pdfDocument.Save(excelPath, saveOptions);

            Console.WriteLine($"PDF successfully converted to Excel: '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
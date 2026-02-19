using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcel
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument or default)
        string pdfPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output XLSX file path (second argument or default)
        string excelPath = args.Length > 1 ? args[1] : "output.xlsx";

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

            // Set Excel save options to generate a single worksheet
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                MinimizeTheNumberOfWorksheets = true, // combine all pages into one sheet
                Format = ExcelSaveOptions.ExcelFormat.XLSX // explicit XLSX format
            };

            // Save the PDF as an Excel workbook
            pdfDocument.Save(excelPath, saveOptions);

            Console.WriteLine($"Conversion successful. Excel file saved to '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions (including ExcelSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination Excel file
        const string inputPdfPath  = "input.pdf";
        const string outputXlsPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize ExcelSaveOptions – required when saving to a non‑PDF format
                ExcelSaveOptions excelOptions = new ExcelSaveOptions();

                // Optional: explicitly set the desired Excel format (XLSX is default)
                // excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX;

                // Save the PDF as an Excel workbook
                pdfDocument.Save(outputXlsPath, excelOptions);
            }

            Console.WriteLine($"PDF successfully converted to Excel: {outputXlsPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or saving
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
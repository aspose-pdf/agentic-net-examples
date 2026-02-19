using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define paths (adjust as needed)
        string dataDir = "Data";
        string pdfPath = Path.Combine(dataDir, "input.pdf");
        string xlsPath = Path.Combine(dataDir, "output.xlsx");

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

            // Configure Excel save options:
            // - MinimizeTheNumberOfWorksheets = true merges all pages into a single worksheet
            // - Format = XLSX specifies the modern Excel file format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                MinimizeTheNumberOfWorksheets = true,
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook with a single worksheet
            pdfDocument.Save(xlsPath, saveOptions);

            Console.WriteLine($"Conversion successful. Excel file saved to: {xlsPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
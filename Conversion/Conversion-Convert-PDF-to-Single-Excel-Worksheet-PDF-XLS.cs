using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcel
{
    static void Main(string[] args)
    {
        // Directory containing the source PDF and where the Excel file will be written.
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "sample.pdf");

        // Output Excel file (single worksheet).
        string excelPath = Path.Combine(dataDir, "output.xlsx");

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(pdfPath);

        // Configure Excel save options to combine all pages into one worksheet.
        ExcelSaveOptions saveOptions = new ExcelSaveOptions
        {
            MinimizeTheNumberOfWorksheets = true
        };

        // Save the PDF as an Excel workbook using the configured options.
        pdfDocument.Save(excelPath, saveOptions);

        Console.WriteLine($"Conversion completed. Excel file saved to: {excelPath}");
    }
}
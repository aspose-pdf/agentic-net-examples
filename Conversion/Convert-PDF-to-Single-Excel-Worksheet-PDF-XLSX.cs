using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination Excel file
        const string inputPdfPath = "input.pdf";
        const string outputExcelPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (creation/load phase)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize save options for Excel conversion
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Combine all PDF pages into a single worksheet
            excelOptions.MinimizeTheNumberOfWorksheets = true;

            // Save the document as an XLSX file using the provided save options (save phase)
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputExcelPath}'");
    }
}
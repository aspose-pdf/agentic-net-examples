using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types (Document, ExcelSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired output Excel file path (XLSX)
        const string outputXlsxPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Enable worksheet minimization: combine pages into as few worksheets as possible
                MinimizeTheNumberOfWorksheets = true
                // The default Excel format is XLSX; no need to set ExcelOptions.ExcelFormat explicitly
            };

            // Save the PDF as an Excel workbook using the configured options
            pdfDocument.Save(outputXlsxPath, excelOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputXlsxPath}'");
    }
}
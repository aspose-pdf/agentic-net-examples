using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, ExcelSaveOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path (replace with your actual file)
        const string inputPdfPath = "input.pdf";

        // Output Excel file path (the format is determined by the file extension)
        const string outputExcelPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create an instance of ExcelSaveOptions using the default constructor
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Optionally, you can adjust properties, e.g.:
            // excelOptions.InsertBlankColumnAtFirst = true;
            // excelOptions.MinimizeTheNumberOfWorksheets = true;

            // Save the PDF as an Excel file using the options instance
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputExcelPath}");
    }
}
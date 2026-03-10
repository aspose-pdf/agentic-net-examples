using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure ExcelSaveOptions to produce CSV output
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();
            excelOptions.Format = ExcelSaveOptions.ExcelFormat.CSV; // CSV format

            // Save the PDF as CSV using the explicit save options
            pdfDocument.Save(outputCsvPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to CSV: {outputCsvPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) or default
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        // Output Excel path (second argument) or default
        string outputPath = args.Length > 1 ? args[1] : "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Configure Excel save options
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Insert a blank column as the first column of each worksheet
                InsertBlankColumnAtFirst = true,
                // Choose the desired Excel format (XLSX is default)
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel file using the configured options
            pdfDocument.Save(outputPath, excelOptions);

            Console.WriteLine($"Conversion successful: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
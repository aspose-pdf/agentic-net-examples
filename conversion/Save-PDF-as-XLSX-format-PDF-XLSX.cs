using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXlsxPath = "output.xlsx";

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
                // Initialize ExcelSaveOptions – defaults to XLSX format
                ExcelSaveOptions excelOptions = new ExcelSaveOptions();

                // Explicitly set the desired Excel format (optional, XLSX is the default)
                excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX;

                // Save the PDF as an Excel workbook
                pdfDocument.Save(outputXlsxPath, excelOptions);
            }

            Console.WriteLine($"PDF successfully converted to XLSX: {outputXlsxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
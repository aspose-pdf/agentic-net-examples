using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output XLSX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToExcelConverter <input.pdf> <output.xlsx>");
            return;
        }

        string pdfPath = args[0];
        string xlsxPath = args[1];

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

            // Configure Excel save options (XLSX is the default format)
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook
            pdfDocument.Save(xlsxPath, saveOptions);

            Console.WriteLine($"Conversion successful. XLSX saved to '{xlsxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
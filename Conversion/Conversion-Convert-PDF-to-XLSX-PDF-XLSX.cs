using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output XLSX path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToExcelConverter <input.pdf> <output.xlsx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputXlsxPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Configure Excel save options – default format is XLSX, but set explicitly.
            var excelOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook.
            pdfDocument.Save(outputXlsxPath, excelOptions);

            Console.WriteLine($"PDF successfully converted to Excel: {outputXlsxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
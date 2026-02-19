using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output Excel path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToExcelConverter <input.pdf> <output.xlsx>");
            return;
        }

        string pdfPath = args[0];
        string excelPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(pdfPath);

            // Configure Excel save options.
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Explicitly set the desired Excel format (XLSX is default, but set for clarity).
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook.
            pdfDocument.Save(excelPath, excelOptions);

            Console.WriteLine($"Conversion successful. Excel file saved to '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}

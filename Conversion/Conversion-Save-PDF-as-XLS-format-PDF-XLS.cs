using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (first argument) and output Excel file path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToExcelConverter <input.pdf> <output.xls>");
            return;
        }

        string pdfPath = args[0];
        string excelPath = args[1];

        // Verify that the input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Save the document as Excel. The file extension determines the format.
            // For .xls use the older Excel format, for .xlsx use the default Office Open XML format.
            pdfDocument.Save(excelPath);

            Console.WriteLine($"PDF successfully converted to Excel and saved at '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
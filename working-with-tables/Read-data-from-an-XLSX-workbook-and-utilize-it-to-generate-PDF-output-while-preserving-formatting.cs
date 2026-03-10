using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // Facades namespace (required by the task)

class Program
{
    static void Main()
    {
        // Paths to the source Excel workbook and the target PDF file.
        const string excelPath = "input.xlsx";
        const string pdfPath   = "output.pdf";

        // Verify that the Excel file exists before attempting conversion.
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Error: Excel file not found at '{excelPath}'.");
            return;
        }

        try
        {
            // Load the XLSX workbook directly into a Document object.
            // Aspose.Pdf can interpret Excel files and preserve their layout/formatting.
            using (Document pdfDocument = new Document(excelPath))
            {
                // Save the loaded content as a PDF file.
                // No additional SaveOptions are required; the default preserves the original formatting.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"Successfully converted '{excelPath}' to PDF at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during loading or saving.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
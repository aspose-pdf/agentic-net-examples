using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF file
        const string outputXls = "output.xls";  // desired Excel output

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize the Excel save options (required for non‑PDF output)
                ExcelSaveOptions excelOpts = new ExcelSaveOptions();

                // Save the PDF as an Excel file using the explicit save options
                pdfDoc.Save(outputXls, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to Excel: '{outputXls}'");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
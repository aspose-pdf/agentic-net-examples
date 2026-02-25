using System;
using System.IO;
using Aspose.Pdf;               // Aspose.Pdf contains Document and ExcelSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF and output Excel file paths
        const string inputPdfPath  = "input.pdf";
        const string outputXlsxPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Create ExcelSaveOptions – required when saving to a non‑PDF format
                ExcelSaveOptions excelOpts = new ExcelSaveOptions();

                // Optionally configure the format (default is XLSX)
                // excelOpts.Format = ExcelSaveOptions.ExcelFormat.XLSX;

                // Save the PDF as an Excel workbook
                pdfDoc.Save(outputXlsxPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputXlsxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf; // Core PDF API – ExcelSaveOptions resides in this namespace

class PdfToXlsConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output XLSX file path (Excel workbook)
        const string outputXlsPath = "output.xlsx";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize ExcelSaveOptions – required when saving to a non‑PDF format
                ExcelSaveOptions excelOptions = new ExcelSaveOptions();

                // Set to minimize the number of worksheets so that all pages go into a single sheet
                excelOptions.MinimizeTheNumberOfWorksheets = true;

                // Optional: insert a blank column at the beginning of the worksheet
                // excelOptions.InsertBlankColumnAtFirst = false; // default is false

                // Save the PDF as an Excel workbook using the options above
                pdfDocument.Save(outputXlsPath, excelOptions);
            }

            Console.WriteLine($"PDF successfully converted to Excel: '{outputXlsPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output XLSX file path (Excel workbook)
        const string outputXls = "output.xlsx";

        // Verify that the source PDF exists
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
                // Configure Excel save options
                ExcelSaveOptions excelOptions = new ExcelSaveOptions
                {
                    // Minimize the number of worksheets so that all pages are placed into a single sheet
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel file using the specified options
                pdfDoc.Save(outputXls, excelOptions);
            }

            Console.WriteLine($"Conversion completed successfully. Excel file saved to '{outputXls}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
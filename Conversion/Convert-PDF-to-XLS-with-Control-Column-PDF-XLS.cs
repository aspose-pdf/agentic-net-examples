using System;
using System.IO;
using Aspose.Pdf; // Core PDF API (ExcelSaveOptions is also in this namespace)

class PdfToExcelConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output Excel file path (XLSX format)
        const string outputXls = "output.xlsx";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure Excel export options
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Insert a blank column as the first column of each worksheet.
                    // This column can be used later as a control column (e.g., for IDs or flags).
                    InsertBlankColumnAtFirst = true,

                    // Optional: minimize the number of worksheets (one worksheet per document instead of per page)
                    // MinimizeTheNumberOfWorksheets = true,

                    // Optional: use uniform column division across all pages
                    // UniformWorksheets = true
                };

                // Save the PDF as an Excel workbook using the configured options
                pdfDoc.Save(outputXls, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to Excel: {outputXls}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

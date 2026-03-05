using System;
using System.IO;
using Aspose.Pdf; // Core PDF classes and save options are in this namespace

class Program
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
                // Configure Excel save options
                var excelOpts = new ExcelSaveOptions
                {
                    // Combine all pages into a single worksheet
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel workbook
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
using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog_min.xlsx";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure Excel save options
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Ensure the output format is XLSX
                    Format = ExcelSaveOptions.ExcelFormat.XLSX,
                    // Minimize the number of worksheets in the resulting workbook
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel workbook using the specified options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
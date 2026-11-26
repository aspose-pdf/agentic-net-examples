using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials_blank.xlsx";

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
                // Configure ExcelSaveOptions:
                // - Set format to XLSX
                // - Enable insertion of a blank column at the first position
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSX,
                    InsertBlankColumnAtFirst = true
                };

                // Save the PDF as an Excel workbook using the configured options
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
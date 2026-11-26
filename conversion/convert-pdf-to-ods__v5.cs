using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials.ods";

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
                // Configure ExcelSaveOptions to produce ODS format
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Explicitly set the output format to ODS
                    Format = ExcelSaveOptions.ExcelFormat.ODS
                };

                // Save the PDF as ODS using the explicit options (required for non‑PDF output)
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
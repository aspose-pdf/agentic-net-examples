using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "summary.pdf";
        const string outputPath = "summary_blank.xlsm";

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
                    // Insert a blank column at the beginning of each sheet
                    InsertBlankColumnAtFirst = true,
                    // Ensure the output format is XLSM (macro-enabled workbook)
                    Format = ExcelSaveOptions.ExcelFormat.XLSM
                };

                // Save the PDF as an XLSM workbook using the specified options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
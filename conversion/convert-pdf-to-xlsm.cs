using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice.xlsm";

        // Verify the source PDF exists
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
                // Configure ExcelSaveOptions to produce an XLSM workbook
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Explicitly set the output format to XLSM (macro-enabled workbook)
                    Format = ExcelSaveOptions.ExcelFormat.XLSM
                };

                // Save the PDF as an XLSM file using the options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during conversion
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
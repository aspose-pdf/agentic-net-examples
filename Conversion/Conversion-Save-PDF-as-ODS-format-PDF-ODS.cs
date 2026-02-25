using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.ods";

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
                // Configure ExcelSaveOptions to produce ODS (OpenDocument Spreadsheet) format
                ExcelSaveOptions saveOptions = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.ODS
                };

                // Save the document using the explicit SaveOptions (required for non‑PDF output)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to ODS: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
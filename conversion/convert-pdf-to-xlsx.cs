using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice.xlsx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions to produce an XLSX file
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Explicitly set the output format to XLSX
                    Format = ExcelSaveOptions.ExcelFormat.XLSX
                };

                // Save the PDF as an Excel workbook using the options above
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
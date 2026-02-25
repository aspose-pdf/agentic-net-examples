using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, ExcelSaveOptions)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "output.csv";

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
                // Create ExcelSaveOptions and set the output format to CSV
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // The nested enum ExcelFormat defines the possible output types (XLS, XLSX, CSV, etc.)
                    Format = ExcelSaveOptions.ExcelFormat.CSV
                };

                // Save the PDF as a CSV file using the configured options
                pdfDoc.Save(outputCsv, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputCsv}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
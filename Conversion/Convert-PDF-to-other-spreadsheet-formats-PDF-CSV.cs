using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class PdfToCsvConverter
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "output.csv";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure ExcelSaveOptions to produce CSV output
                ExcelSaveOptions csvOptions = new ExcelSaveOptions
                {
                    // Explicitly set the desired format to CSV
                    Format = ExcelSaveOptions.ExcelFormat.CSV
                };

                // Save the PDF as CSV using the explicit SaveOptions
                pdfDoc.Save(outputCsvPath, csvOptions);
            }

            Console.WriteLine($"PDF successfully converted to CSV: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
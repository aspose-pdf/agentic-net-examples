using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, ExcelSaveOptions, etc.)

class PdfToCsvConverter
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.csv";

        // Verify that the source PDF exists before attempting conversion.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce CSV output.
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                // Explicitly set the format to CSV; otherwise Save would default to PDF.
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV using the options defined above.
            pdfDoc.Save(outputPath, csvOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}
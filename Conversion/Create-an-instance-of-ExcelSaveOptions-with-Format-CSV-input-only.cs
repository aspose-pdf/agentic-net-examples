using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement, though not directly used here.

class Program
{
    static void Main()
    {
        // Paths for input PDF and output CSV file.
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "output.csv";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create ExcelSaveOptions and set the output format to CSV.
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the PDF using the configured options.
            pdfDocument.Save(outputCsvPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to CSV at '{outputCsvPath}'.");
    }
}
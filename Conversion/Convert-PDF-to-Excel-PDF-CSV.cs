using System;
using System.IO;
using Aspose.Pdf;

class PdfToCsvConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output CSV file path
        const string outputCsvPath = "output.csv";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize ExcelSaveOptions and specify CSV as the desired format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Set the output format to CSV
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the PDF content as a CSV file using the specified options
            pdfDocument.Save(outputCsvPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to CSV: {outputCsvPath}");
    }
}
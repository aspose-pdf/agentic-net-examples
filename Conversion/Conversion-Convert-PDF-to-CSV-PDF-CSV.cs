using System;
using System.IO;
using Aspose.Pdf;

class PdfToCsvConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output CSV path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToCsvConverter <input.pdf> <output.csv>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPath);

            // Configure ExcelSaveOptions to produce CSV output.
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // The correct property is 'Format', not 'ExcelFormat'.
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the PDF content as CSV.
            pdfDocument.Save(outputPath, saveOptions);

            Console.WriteLine($"PDF successfully converted to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
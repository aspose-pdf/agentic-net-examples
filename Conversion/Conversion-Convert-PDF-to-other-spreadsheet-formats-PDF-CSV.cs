using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions and its nested ExcelFormat enum are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Desired CSV output file path
        const string outputCsvPath = "output.csv";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure ExcelSaveOptions to produce CSV output
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Set the output format to CSV using the nested enum
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the PDF content as a CSV file
            pdfDocument.Save(outputCsvPath, excelOptions);

            Console.WriteLine($"PDF successfully converted to CSV and saved at '{outputCsvPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}

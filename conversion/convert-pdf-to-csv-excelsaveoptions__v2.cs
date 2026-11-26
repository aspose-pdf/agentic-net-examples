using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "specifications.pdf";
        const string outputPath = "specifications.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and convert it to CSV using ExcelSaveOptions
        using (Document pdfDoc = new Document(inputPath))
        {
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Explicitly set the output format to CSV
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV; passing the options ensures non‑PDF output
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"PDF successfully converted to CSV: {outputPath}");
    }
}
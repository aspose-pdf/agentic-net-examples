using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.csv";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an instance of ExcelSaveOptions
            ExcelSaveOptions excelOpts = new ExcelSaveOptions();

            // Set the desired output format to CSV
            excelOpts.Format = ExcelSaveOptions.ExcelFormat.CSV;

            // Save the document as CSV using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"CSV file saved to '{outputPath}'.");
    }
}
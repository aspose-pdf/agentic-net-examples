using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions are in this namespace
using Aspose.Pdf.Text;          // Required for text extraction if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to output CSV format
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the PDF content as CSV using the specified options
            doc.Save(outputPath, csvOptions);
        }

        Console.WriteLine($"PDF converted to CSV successfully: {outputPath}");
    }
}
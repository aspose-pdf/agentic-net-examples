using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "TableExport.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions to output CSV format.
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.CSV
                };

                // Save the document as CSV. The Save method with SaveOptions ensures
                // the output format follows the specified options (CSV in this case).
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"CSV export completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
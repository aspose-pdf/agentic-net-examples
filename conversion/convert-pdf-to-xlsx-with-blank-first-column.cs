using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";
        const string outputPath = "invoice_blank.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Ensure the output format is XLSX
                Format = ExcelSaveOptions.ExcelFormat.XLSX,
                // Insert a blank column at the first position
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an XLSX file using the specified options
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
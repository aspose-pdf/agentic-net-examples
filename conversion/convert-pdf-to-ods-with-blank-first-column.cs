using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice_blank.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options: ODS format and insert a blank column at the first position
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                InsertBlankColumnAtFirst = true,
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the PDF as ODS using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
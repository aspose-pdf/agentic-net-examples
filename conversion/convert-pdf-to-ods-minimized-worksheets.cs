using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice_min.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options for ODS output
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the target format to ODS
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                // Minimize the number of worksheets in the resulting ODS file
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the PDF as ODS using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
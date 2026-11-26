using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions classes are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report_min.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce ODS and minimize worksheets
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format                     = ExcelSaveOptions.ExcelFormat.ODS,
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the document as ODS using the configured options
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
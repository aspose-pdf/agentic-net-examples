using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce ODS format
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the document as ODS
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
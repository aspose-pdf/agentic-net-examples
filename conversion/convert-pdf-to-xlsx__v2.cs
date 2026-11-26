using System;
using System.IO;
using Aspose.Pdf;               // Aspose.Pdf namespace contains Document and all SaveOptions classes

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report.xlsx";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce XLSX format
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF content as an Excel workbook (XLSX)
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}
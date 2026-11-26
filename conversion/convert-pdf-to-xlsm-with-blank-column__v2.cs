using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report_blank.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Save as XLSM (macro-enabled workbook)
                Format = ExcelSaveOptions.ExcelFormat.XLSM,
                // Insert a blank column at the first position of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an XLSM file using the specified options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
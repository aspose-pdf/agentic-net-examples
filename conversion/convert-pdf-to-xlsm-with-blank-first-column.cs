using System;
using System.IO;
using Aspose.Pdf;   // Document and ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_blank.xlsm";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options:
            // - Insert a blank column as the first column in each sheet
            // - Set the output format to XLSM (macro‑enabled workbook)
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                InsertBlankColumnAtFirst = true,
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the PDF as an XLSM workbook using the configured options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
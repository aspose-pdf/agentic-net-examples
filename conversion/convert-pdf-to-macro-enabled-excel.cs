using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "specifications.pdf";
        const string outputPath = "specifications.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Set up ExcelSaveOptions to produce a macro‑enabled XLSM workbook
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSM
                };

                // Save the PDF as XLSM using the specified options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion succeeded: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
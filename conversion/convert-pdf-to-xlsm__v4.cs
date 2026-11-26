using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "data_sheet.pdf";
        const string outputPath = "data_sheet.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Set up ExcelSaveOptions to produce an XLSM workbook
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSM
                };

                // Save the PDF as XLSM
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion succeeded: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
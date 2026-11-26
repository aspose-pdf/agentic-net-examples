using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";
        const string outputPath = "invoice_blank.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    InsertBlankColumnAtFirst = true,
                    Format = ExcelSaveOptions.ExcelFormat.XLSM
                };
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to XLSM with a blank column: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
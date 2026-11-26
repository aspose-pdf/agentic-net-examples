using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "data_sheet.pdf";
        const string outputPath = "data_sheet_blank.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ExcelSaveOptions options = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSM,
                    InsertBlankColumnAtFirst = true
                };

                doc.Save(outputPath, options);
            }

            Console.WriteLine($"PDF converted and saved as XLSM: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
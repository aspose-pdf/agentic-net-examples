using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "summary.pdf";
        const string outputPath = "summary_blank.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                ExcelSaveOptions opts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.ODS,
                    InsertBlankColumnAtFirst = true
                };
                doc.Save(outputPath, opts);
            }

            Console.WriteLine($"PDF converted to ODS with blank column: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "report.pdf";
        const string outputPath = "report_blank.ods";

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
                // Configure Excel save options for ODS output
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    InsertBlankColumnAtFirst = true,                     // Enable blank column at the start
                    Format = ExcelSaveOptions.ExcelFormat.ODS           // Set output format to ODS
                };

                // Save as ODS using the specified options
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Converted PDF to ODS: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "data_sheet.pdf";
        const string outputPath = "data_sheet_min.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure Excel save options: ODS format and minimize worksheets
                ExcelSaveOptions saveOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.ODS,
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as ODS using the specified options
                doc.Save(outputPath, saveOpts);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
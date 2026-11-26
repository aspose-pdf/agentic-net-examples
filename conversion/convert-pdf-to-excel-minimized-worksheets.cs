using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials_min.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Configure Excel save options: XLSX format and minimize worksheets
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSX,
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel file using the specified options
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
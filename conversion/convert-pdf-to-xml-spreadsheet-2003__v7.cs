using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including ExcelSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions to produce XML Spreadsheet 2003 format
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
                };

                // Save the document using the explicit SaveOptions (required for non‑PDF output)
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";
        const string outputPath = "invoice.xml";

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
                // Configure ExcelSaveOptions to produce XML Spreadsheet 2003 format
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
                };

                // Save the document as XML Spreadsheet
                doc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to XML Spreadsheet: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
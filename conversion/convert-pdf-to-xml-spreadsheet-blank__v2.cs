using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials_blank.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure Excel save options for XML Spreadsheet 2003
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the output format to XML Spreadsheet 2003
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                // Insert a blank column at the beginning of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the document as XML Spreadsheet 2003
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_blank.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for XML Spreadsheet 2003 output
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the target format to XML Spreadsheet 2003
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                // Insert a blank column at the first position of each table
                InsertBlankColumnAtFirst = true
            };

            // Save the document as XML Spreadsheet 2003 using the configured options
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions classes are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "report.pdf";
        const string outputPath = "report_blank.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for XML Spreadsheet 2003
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Set the output format to XML Spreadsheet 2003
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                // Insert a blank column at the beginning of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the document as XML Spreadsheet using the configured options
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
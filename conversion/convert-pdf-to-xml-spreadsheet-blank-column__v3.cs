using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "data_sheet.pdf";
        const string outputPath = "data_sheet_blank.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce XML Spreadsheet 2003 format
            // and insert a blank column at the beginning of each table.
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                InsertBlankColumnAtFirst = true
            };

            // Save the document as XML Spreadsheet 2003.
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
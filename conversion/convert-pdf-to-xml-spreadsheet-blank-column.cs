using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice_blank.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for XML Spreadsheet 2003 output
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Specify the exact Excel format required
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                // Insert a blank column at the beginning of each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the PDF as an XML Spreadsheet 2003 file using the options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
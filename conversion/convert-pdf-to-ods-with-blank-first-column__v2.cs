using System;
using System.IO;
using Aspose.Pdf; // Contains Document and all SaveOptions subclasses

class Program
{
    static void Main()
    {
        const string inputPath  = "data_sheet.pdf";
        const string outputPath = "data_sheet_blank.ods";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for ODS output
            ExcelSaveOptions saveOpts = new ExcelSaveOptions
            {
                // Set the target format to ODS
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                // Insert a blank column as the first column in each sheet
                InsertBlankColumnAtFirst = true
            };

            // Save the document as ODS using the configured options
            doc.Save(outputPath, saveOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
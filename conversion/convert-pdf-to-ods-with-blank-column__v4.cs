using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_blank.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce ODS and insert a blank column at the start of each sheet
            ExcelSaveOptions saveOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                InsertBlankColumnAtFirst = true
            };

            // Save the document as ODS using the configured options
            doc.Save(outputPath, saveOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_min.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions for ODS output
            ExcelSaveOptions saveOpts = new ExcelSaveOptions
            {
                // Set the target format to ODS
                Format = ExcelSaveOptions.ExcelFormat.ODS,
                // Minimize the number of worksheets in the resulting file
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the document as ODS using the configured options
            doc.Save(outputPath, saveOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
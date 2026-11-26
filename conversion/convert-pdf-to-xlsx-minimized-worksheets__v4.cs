using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_min.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Configure Excel save options: minimize worksheets and enforce XLSX format
        ExcelSaveOptions excelOpts = new ExcelSaveOptions
        {
            MinimizeTheNumberOfWorksheets = true,
            Format = ExcelSaveOptions.ExcelFormat.XLSX
        };

        // Load the PDF and save it as an Excel file using the options above
        using (Document doc = new Document(inputPath))
        {
            doc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
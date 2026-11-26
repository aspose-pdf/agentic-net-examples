using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "specifications.pdf";
        const string outputPath = "specifications_blank.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options: insert a blank column at the first position
            // and ensure the output format is XLSX.
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                InsertBlankColumnAtFirst = true,
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an XLSX file using the configured options.
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
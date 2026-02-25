using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options to generate a single worksheet
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                MinimizeTheNumberOfWorksheets = true // combine all pages into one sheet
            };

            // Save the document as Excel using the explicit options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputPath}");
    }
}
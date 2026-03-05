using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options to produce a macro‑enabled XLSM workbook
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the PDF as XLSM using the specified options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: {outputPath}");
    }
}
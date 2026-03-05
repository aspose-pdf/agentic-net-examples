using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xlsm";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create Excel save options and specify the macro‑enabled XLSM format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the PDF as an XLSM workbook
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: {outputPath}");
    }
}
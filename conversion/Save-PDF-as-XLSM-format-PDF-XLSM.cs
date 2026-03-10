using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize ExcelSaveOptions and specify the XLSM format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.XLSM;

            // Save the PDF as an XLSM workbook using the explicit save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully saved as XLSM to '{outputPath}'.");
    }
}
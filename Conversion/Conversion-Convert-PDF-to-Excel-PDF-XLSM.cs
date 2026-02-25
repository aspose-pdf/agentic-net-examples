using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXlsmPath = "output.xlsm";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure Excel save options to produce a macro‑enabled XLSM workbook.
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the PDF as XLSM using the explicit save options.
            pdfDoc.Save(outputXlsmPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: {outputXlsmPath}");
    }
}
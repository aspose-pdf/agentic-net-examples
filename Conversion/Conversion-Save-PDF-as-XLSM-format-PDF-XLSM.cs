using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xlsm";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure Excel save options to produce a macro‑enabled XLSM file
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the document as XLSM using the explicit SaveOptions
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: '{outputPath}'");
    }
}
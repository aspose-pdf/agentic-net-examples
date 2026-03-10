using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output Excel file path (macro‑enabled .xlsm)
        const string outputPath = "output.xlsm";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create ExcelSaveOptions instance
            ExcelSaveOptions excelOpts = new ExcelSaveOptions();

            // Set the output format to macro‑enabled XLSM
            excelOpts.Format = ExcelSaveOptions.ExcelFormat.XLSM;

            // Save the PDF as an XLSM file using the specified options
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Excel file saved to '{outputPath}'.");
    }
}
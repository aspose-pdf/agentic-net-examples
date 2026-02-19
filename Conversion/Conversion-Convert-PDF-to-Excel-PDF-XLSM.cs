using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        string inputPath = "input.pdf";
        // Desired output Excel file path (macro‑enabled .xlsm)
        string outputPath = "output.xlsm";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Configure Excel save options to produce an .xlsm file
        ExcelSaveOptions excelOptions = new ExcelSaveOptions
        {
            // Set the output format to macro‑enabled workbook
            Format = ExcelSaveOptions.ExcelFormat.XLSM
        };

        // Save the PDF as an Excel workbook using the specified options
        pdfDocument.Save(outputPath, excelOptions);

        Console.WriteLine($"Conversion completed successfully. Output saved to: {outputPath}");
    }
}
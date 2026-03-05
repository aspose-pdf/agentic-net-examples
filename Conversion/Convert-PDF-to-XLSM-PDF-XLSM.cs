using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, ExcelSaveOptions.ExcelFormat

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output XLSM file path
        const string outputXlsmPath = "output.xlsm";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document, convert and save as XLSM
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure Excel save options for macro‑enabled workbook (XLSM)
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Specify the desired Excel format
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the document using the explicit save options
            pdfDocument.Save(outputXlsmPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: {outputXlsmPath}");
    }
}
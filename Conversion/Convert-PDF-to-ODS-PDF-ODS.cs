using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Paths for source PDF and destination ODS file
        const string inputPdfPath  = "input.pdf";
        const string outputOdsPath = "output.ods";

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, convert and save as ODS using ExcelSaveOptions
        using (Document pdfDoc = new Document(inputPdfPath))   // deterministic disposal
        {
            // Configure save options to produce ODS format
            ExcelSaveOptions odsOptions = new ExcelSaveOptions
            {
                // The ExcelFormat enum includes ODS (OpenDocument Spreadsheet)
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the document; passing SaveOptions ensures non‑PDF output
            pdfDoc.Save(outputOdsPath, odsOptions);
        }

        Console.WriteLine($"PDF successfully converted to ODS: {outputOdsPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputOds = "output.ods";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure ExcelSaveOptions to export as ODS (OpenDocument Spreadsheet)
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.ODS;

            // Save the document using explicit save options (required for non‑PDF formats)
            pdfDoc.Save(outputOds, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to ODS: {outputOds}");
    }
}
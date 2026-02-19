using System;
using System.IO;
using Aspose.Pdf;

class PdfToOdsConverter
{
    static void Main(string[] args)
    {
        // Input PDF path and output ODS path can be passed as arguments or hard‑coded.
        string inputPdfPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputOdsPath = args.Length > 1 ? args[1] : "output.ods";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Configure Excel save options to produce ODS format.
            var excelOptions = new ExcelSaveOptions
            {
                // The ExcelFormat enum is nested inside ExcelSaveOptions.
                Format = ExcelSaveOptions.ExcelFormat.ODS // OpenDocument Spreadsheet
            };

            // Save the document as ODS.
            pdfDocument.Save(outputOdsPath, excelOptions);

            Console.WriteLine($"Conversion successful. ODS saved to: {outputOdsPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

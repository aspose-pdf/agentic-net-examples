using System;
using System.IO;
using Aspose.Pdf;

class PdfToXlsmConverter
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToXlsmConverter <input-pdf-path> <output-xlsm-path>");
            return;
        }

        string pdfPath = args[0];
        string xlsmPath = args[1];

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Configure Excel save options to produce a macro‑enabled XLSM file
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the document as XLSM
            pdfDocument.Save(xlsmPath, saveOptions);

            Console.WriteLine($"PDF successfully converted to XLSM: {xlsmPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}

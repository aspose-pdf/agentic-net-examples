using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions resides directly in this namespace

class PdfToXlsmConverter
{
    static void Main(string[] args)
    {
        // Expect input PDF path and output XLSM path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToXlsmConverter <input.pdf> <output.xlsm>");
            return;
        }

        string inputPdfPath = args[0];
        string outputXlsmPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Configure Excel save options to produce a macro‑enabled XLSM file.
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSM
            };

            // Save the PDF as XLSM using the configured options.
            pdfDocument.Save(outputXlsmPath, excelOptions);

            Console.WriteLine($"Conversion successful. XLSM saved to: {outputXlsmPath}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
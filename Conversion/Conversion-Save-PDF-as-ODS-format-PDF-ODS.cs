using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions resides directly in this namespace

class PdfToOdsConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output ODS path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToOdsConverter <input.pdf> <output.ods>");
            return;
        }

        string inputPdfPath = args[0];
        string outputOdsPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure Excel save options to produce ODS format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Use the nested ExcelFormat enum inside ExcelSaveOptions
                Format = ExcelSaveOptions.ExcelFormat.ODS // ODS (OpenDocument Spreadsheet)
            };

            // Save the document as ODS
            pdfDocument.Save(outputOdsPath, saveOptions);

            Console.WriteLine($"Conversion successful. ODS file saved to '{outputOdsPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}

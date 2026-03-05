using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputOdsPath = "output.ods";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure ExcelSaveOptions to produce an ODS file.
                ExcelSaveOptions saveOpts = new ExcelSaveOptions
                {
                    // The ExcelFormat enum includes ODS (OpenDocument Spreadsheet).
                    Format = ExcelSaveOptions.ExcelFormat.ODS
                };

                // Save the PDF as ODS using the options above.
                pdfDoc.Save(outputOdsPath, saveOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputOdsPath}'");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during conversion.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
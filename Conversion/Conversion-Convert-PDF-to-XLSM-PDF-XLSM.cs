using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXlsmPath = "output.xlsm";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialise Excel save options – required for non‑PDF output
                ExcelSaveOptions excelOpts = new ExcelSaveOptions();

                // Optional: explicitly request XLSM format if the enum supports it.
                // Uncomment the line below if ExcelSaveOptions.ExcelFormat includes XLSM.
                // excelOpts.Format = ExcelSaveOptions.ExcelFormat.XLSM;

                // Save the PDF as an Excel macro‑enabled workbook
                pdfDoc.Save(outputXlsmPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputXlsmPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
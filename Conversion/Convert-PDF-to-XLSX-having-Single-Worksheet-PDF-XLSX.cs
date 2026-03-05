using System;
using System.IO;
using Aspose.Pdf;   // Document, ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXlsx = "output.xlsx";

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Prepare Excel save options (must pass explicit options for non‑PDF output)
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Minimize the number of worksheets → all pages go into a single sheet
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save as XLSX using the options (lifecycle rule: save inside using block)
                pdfDoc.Save(outputXlsx, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to a single‑worksheet XLSX: {outputXlsx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
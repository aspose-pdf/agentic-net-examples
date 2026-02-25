using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per task requirement

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXlsx = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize ExcelSaveOptions – required for non‑PDF output
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Ensure the entire PDF is saved into a single worksheet
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an XLSX file using the explicit options
                pdfDoc.Save(outputXlsx, excelOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputXlsx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
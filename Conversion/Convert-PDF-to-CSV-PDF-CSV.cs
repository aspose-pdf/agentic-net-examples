using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputCsv = "output.csv";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure ExcelSaveOptions to produce CSV output.
                ExcelSaveOptions csvOptions = new ExcelSaveOptions();
                csvOptions.Format = ExcelSaveOptions.ExcelFormat.CSV; // CSV format

                // Save the document as CSV using the explicit SaveOptions.
                pdfDoc.Save(outputCsv, csvOptions);
            }

            Console.WriteLine($"PDF successfully converted to CSV: '{outputCsv}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document and ExcelSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputCsvPath = "output.csv";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure save options to produce CSV output
                ExcelSaveOptions csvOptions = new ExcelSaveOptions();
                csvOptions.Format = ExcelSaveOptions.ExcelFormat.CSV; // CSV format

                // Save the document as CSV
                pdfDoc.Save(outputCsvPath, csvOptions);
            }

            Console.WriteLine($"PDF successfully converted to CSV: {outputCsvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
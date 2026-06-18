using System;
using System.IO;
using Aspose.Pdf;

class ExportTableToCsv
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF containing tables
        const string outputCsv = "tables.csv"; // destination CSV file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure ExcelSaveOptions to produce CSV output
                ExcelSaveOptions csvOptions = new ExcelSaveOptions
                {
                    // Explicitly set the format to CSV using the nested enum
                    Format = ExcelSaveOptions.ExcelFormat.CSV
                };

                // Save the document as CSV; Aspose.Pdf will export table data accordingly
                pdfDoc.Save(outputCsv, csvOptions);
            }

            Console.WriteLine($"Table data exported to CSV: '{outputCsv}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
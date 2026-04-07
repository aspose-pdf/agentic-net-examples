using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf;               // ExcelSaveOptions is in the same namespace

class Program
{
    static void Main()
    {
        // Input PDF containing a table
        const string inputPdf = "input.pdf";

        // Desired CSV output file
        const string outputCsv = "output.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure ExcelSaveOptions to export as CSV
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                // Specify CSV format explicitly
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV (lifecycle: save)
            pdfDoc.Save(outputCsv, csvOptions);
        }

        Console.WriteLine($"Table data exported to CSV: '{outputCsv}'");
    }
}
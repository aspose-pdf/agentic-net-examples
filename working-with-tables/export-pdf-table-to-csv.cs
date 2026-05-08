using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "output.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Configure ExcelSaveOptions to export as CSV (non‑PDF format requires explicit options)
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV; each table cell is written as a comma‑separated value
            doc.Save(outputCsv, csvOptions);
        }

        Console.WriteLine($"CSV file created at '{outputCsv}'.");
    }
}
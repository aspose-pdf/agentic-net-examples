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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure save options to export as CSV
            ExcelSaveOptions csvOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.CSV
            };

            // Save the document as CSV
            doc.Save(outputCsv, csvOptions);
        }

        Console.WriteLine($"CSV file created at '{outputCsv}'.");
    }
}
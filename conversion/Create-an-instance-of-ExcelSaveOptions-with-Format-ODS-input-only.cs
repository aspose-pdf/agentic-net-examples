using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions is in this namespace

class Program
{
    static void Main()
    {
        // Input PDF path (replace with actual file)
        const string inputPdf = "input.pdf";

        // Output ODS path
        const string outputOds = "output.ods";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create ExcelSaveOptions and set the format to ODS
        ExcelSaveOptions excelOptions = new ExcelSaveOptions
        {
            Format = ExcelSaveOptions.ExcelFormat.ODS // OpenDocument Spreadsheet
        };

        // Load the PDF, then save using the configured options
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputOds, excelOptions);
        }

        Console.WriteLine($"PDF saved as ODS to '{outputOds}'.");
    }
}
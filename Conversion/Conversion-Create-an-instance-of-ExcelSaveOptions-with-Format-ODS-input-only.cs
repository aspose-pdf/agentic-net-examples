using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create ExcelSaveOptions and set the output format to ODS (OpenDocument Spreadsheet)
        ExcelSaveOptions excelOptions = new ExcelSaveOptions
        {
            Format = ExcelSaveOptions.ExcelFormat.ODS
        };

        // Example usage: convert a PDF to ODS using the options above
        const string inputPdf = "input.pdf";
        const string outputOds = "output.ods";

        if (File.Exists(inputPdf))
        {
            // Wrap the Document in a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Save the PDF as ODS using the configured ExcelSaveOptions
                pdfDoc.Save(outputOds, excelOptions);
            }

            Console.WriteLine($"PDF successfully saved as ODS to '{outputOds}'.");
        }
        else
        {
            Console.WriteLine($"Input file not found: {inputPdf}");
        }
    }
}
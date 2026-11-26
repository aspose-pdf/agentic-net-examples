using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXlsx = "tables.xlsx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure ExcelSaveOptions to export as XLSX
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                // Explicitly set the output format to XLSX
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF (including tables) to an XLSX workbook
            pdfDoc.Save(outputXlsx, excelOpts);
        }

        Console.WriteLine($"PDF tables exported to XLSX workbook: {outputXlsx}");
    }
}
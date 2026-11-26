using System;
using System.IO;
using Aspose.Pdf;               // Aspose.Pdf namespace contains Document and ExcelSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF file
        const string outputXlsxPath = "Tables.xlsx";   // destination Excel file

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure ExcelSaveOptions to produce XLSX format
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF content as an Excel workbook (XLSX)
            pdfDoc.Save(outputXlsxPath, excelOpts);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputXlsxPath}");
    }
}
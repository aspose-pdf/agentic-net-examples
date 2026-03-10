using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputExcel = "output.xlsx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize ExcelSaveOptions and configure it to produce a single worksheet
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                MinimizeTheNumberOfWorksheets = true // combine all pages into one worksheet
            };

            // Save the PDF as an Excel file using the explicit save options
            pdfDoc.Save(outputExcel, excelOpts);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {outputExcel}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdfPath = "input.pdf";
        // Desired ODS output file
        const string outputOdsPath = "output.ods";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create ExcelSaveOptions instance
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Set the output format to ODS (OpenDocument Spreadsheet)
            excelOptions.Format = ExcelSaveOptions.ExcelFormat.ODS;

            // Save the PDF as ODS using the configured options
            pdfDoc.Save(outputOdsPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully saved as ODS to '{outputOdsPath}'.");
    }
}
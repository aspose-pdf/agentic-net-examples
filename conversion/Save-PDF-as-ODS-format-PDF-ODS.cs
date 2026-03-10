using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions classes, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputOds = "output.ods";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create ExcelSaveOptions and specify the ODS format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.ODS;   // ODS = OpenDocument Spreadsheet

            // Save the document as ODS using the options
            pdfDoc.Save(outputOds, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to ODS: {outputOds}");
    }
}
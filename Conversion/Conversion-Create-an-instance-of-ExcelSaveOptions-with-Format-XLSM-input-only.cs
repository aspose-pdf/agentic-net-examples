using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXlsm = "output.xlsm";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create ExcelSaveOptions and set the output format to XLSM (macro‑enabled Excel)
        ExcelSaveOptions saveOptions = new ExcelSaveOptions
        {
            Format = ExcelSaveOptions.ExcelFormat.XLSM
        };

        // Load the PDF and save it using the configured ExcelSaveOptions
        using (Document pdfDoc = new Document(inputPdf))
        {
            pdfDoc.Save(outputXlsm, saveOptions);
        }

        Console.WriteLine($"Saved XLSM file to '{outputXlsm}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Output XLSM file path
        const string outputXlsm = "output.xlsm";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document and save it as XLSM
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize ExcelSaveOptions
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Specify the XLSM (macro‑enabled) format
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.XLSM;

            // Save the document using the options
            pdfDocument.Save(outputXlsm, saveOptions);
        }

        Console.WriteLine($"PDF successfully saved as XLSM to '{outputXlsm}'.");
    }
}
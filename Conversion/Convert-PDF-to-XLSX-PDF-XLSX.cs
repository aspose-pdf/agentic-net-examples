using System;
using System.IO;
using Aspose.Pdf;               // Document, ExcelSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";

        // Desired output XLSX file path
        const string outputXlsx = "output.xlsx";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF, convert to XLSX, and save
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize save options for Excel format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Save the document as XLSX using the options
            pdfDocument.Save(outputXlsx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSX: {outputXlsx}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdf = "input.pdf";

        // Desired path for the resulting XLSX file.
        const string outputXlsx = "output.xlsx";

        // Verify that the input file exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize ExcelSaveOptions to control the Excel export.
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Set this flag to true to minimize the number of worksheets,
            // resulting in a single worksheet for the entire PDF.
            saveOptions.MinimizeTheNumberOfWorksheets = true;

            // Save the PDF as an XLSX file using the specified options.
            pdfDocument.Save(outputXlsx, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSX: {outputXlsx}");
    }
}
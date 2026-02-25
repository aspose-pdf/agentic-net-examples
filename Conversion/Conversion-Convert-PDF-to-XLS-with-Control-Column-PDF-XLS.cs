using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXls = "output.xlsx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure Excel save options to insert a blank control column as the first column
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                InsertBlankColumnAtFirst = true
                // Additional options can be set here if needed, e.g. UniformWorksheets = true;
            };

            // Save the PDF as an Excel file using the configured options
            pdfDoc.Save(outputXls, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLS with control column: {outputXls}");
    }
}
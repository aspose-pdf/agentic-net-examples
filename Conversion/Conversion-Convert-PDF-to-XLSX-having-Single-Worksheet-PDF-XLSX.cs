using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions is in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output XLSX file path (single worksheet)
        const string xlsxPath = "output.xlsx";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure Excel save options
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Minimize the number of worksheets so all pages go into one sheet
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the PDF as an XLSX file using the specified options
            pdfDocument.Save(xlsxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to single‑worksheet XLSX: '{xlsxPath}'.");
    }
}
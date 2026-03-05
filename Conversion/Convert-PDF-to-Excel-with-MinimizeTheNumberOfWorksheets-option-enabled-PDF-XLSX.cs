using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document and ExcelSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output Excel file path (XLSX)
        const string excelPath = "output.xlsx";

        // Verify input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document, convert and save as Excel with minimized worksheets
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Configure Excel save options
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Combine content into as few worksheets as possible
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the document to Excel format using the explicit options
            pdfDocument.Save(excelPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel with minimized worksheets: '{excelPath}'.");
    }
}
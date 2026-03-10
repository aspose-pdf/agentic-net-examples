using System;
using System.IO;
using Aspose.Pdf; // Document and ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output Excel file path (XLSX)
        const string excelPath = "output.xlsx";

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF, configure Excel save options, and save as XLSX
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize ExcelSaveOptions and enable worksheet minimization
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the document as an Excel workbook using the explicit options
            pdfDocument.Save(excelPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {excelPath}");
    }
}
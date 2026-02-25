using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the source PDF and where the XLS will be written
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file
        string pdfPath = Path.Combine(dataDir, "input.pdf");
        // Output Excel file (can be .xls, .xlsx, .csv, etc.)
        string xlsPath = Path.Combine(dataDir, "output.xlsx");

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Initialize ExcelSaveOptions – required when saving to a non‑PDF format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // Optional: explicitly set the desired Excel format
                // Format = ExcelSaveOptions.ExcelFormat.XLSX
            };

            // Save the PDF as an Excel workbook
            pdfDoc.Save(xlsPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to Excel: {xlsPath}");
    }
}
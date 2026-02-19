using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Use the application's base directory as the data folder.
            // This avoids hard‑coded placeholders that cause DirectoryNotFoundException.
            string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = Path.Combine(dataDir, "input.pdf");
            string excelPath = Path.Combine(dataDir, "output.xml");

            // Verify that the source PDF exists before attempting to load it.
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
                return;
            }

            // Configure Excel save options (Excel 2003 XML format).
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
            };

            // Load the PDF and save it as an Excel XML spreadsheet.
            using (Document pdfDocument = new Document(pdfPath))
            {
                pdfDocument.Save(excelPath, excelOptions);
                Console.WriteLine($"PDF successfully saved as Excel XML to '{excelPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

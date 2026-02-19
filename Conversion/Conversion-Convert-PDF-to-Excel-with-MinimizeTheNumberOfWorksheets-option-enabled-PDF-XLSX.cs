using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelConverter
{
    static void Main()
    {
        // Define the directory that contains the source PDF.
        // Replace with an actual path when running the program.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        // Input PDF file.
        string pdfPath = Path.Combine(dataDir, "sample.pdf");

        // Output Excel file (XLSX).
        string excelPath = Path.Combine(dataDir, "sample.xlsx");

        // Verify that the input file exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure Excel save options.
                ExcelSaveOptions saveOptions = new ExcelSaveOptions
                {
                    // Minimize the number of worksheets in the resulting workbook.
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel workbook using the configured options.
                pdfDocument.Save(excelPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to Excel. Output saved at '{excelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
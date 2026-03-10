using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output Excel file path (XLSX)
        const string outputPath = "output.xlsx";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create Excel save options (lifecycle: create)
                ExcelSaveOptions saveOptions = new ExcelSaveOptions
                {
                    // Combine all pages into a single worksheet
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel workbook (lifecycle: save)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to a single Excel worksheet: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
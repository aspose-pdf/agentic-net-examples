using System;
using System.IO;
using Aspose.Pdf; // ExcelSaveOptions resides directly in Aspose.Pdf namespace

class PdfToExcelConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output Excel file path (single worksheet)
        const string outputExcelPath = "output.xls";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure Excel save options
            ExcelSaveOptions excelOptions = new ExcelSaveOptions
            {
                // Combine all pages into one worksheet
                MinimizeTheNumberOfWorksheets = true
            };

            // Save the PDF as a single Excel worksheet
            pdfDocument.Save(outputExcelPath, excelOptions);
        }

        Console.WriteLine($"PDF successfully converted to a single Excel worksheet: {outputExcelPath}");
    }
}

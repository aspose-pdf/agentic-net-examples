using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "invoice.pdf";
        const string outputPath = "invoice_min.xlsx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure Excel save options: XLSX format and minimize worksheets
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XLSX,
                    MinimizeTheNumberOfWorksheets = true
                };

                // Save the PDF as an Excel file using the specified options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
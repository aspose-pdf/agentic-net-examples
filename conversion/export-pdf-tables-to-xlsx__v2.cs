using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, ExcelSaveOptions, etc.)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Source PDF containing tables
        const string outputXlsxPath = "TableExport.xlsx"; // Desired XLSX output file

        // Verify the input file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure ExcelSaveOptions to export as XLSX
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Explicitly set the format to XLSX (required for non‑PDF output)
                    Format = ExcelSaveOptions.ExcelFormat.XLSX,

                    // Optional: preserve the original table structure when possible
                    // ExportTableStructure = true
                };

                // Save the PDF content (tables) to an XLSX file using the options above
                pdfDoc.Save(outputXlsxPath, excelOpts);
            }

            Console.WriteLine($"Successfully exported tables to '{outputXlsxPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}
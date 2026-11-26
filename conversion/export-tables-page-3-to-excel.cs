using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API and SaveOptions are here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "Page3Tables.xlsx";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF (wrapped in using for deterministic disposal)
            using (Document srcDoc = new Document(inputPath))
            {
                // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
                if (srcDoc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                    return;
                }

                // Create a new PDF that will contain only page 3
                using (Document pageDoc = new Document())
                {
                    // Add page 3 from the source document to the new document
                    pageDoc.Pages.Add(srcDoc.Pages[3]);

                    // Prepare Excel save options (must be passed explicitly for non‑PDF output)
                    ExcelSaveOptions excelOpts = new ExcelSaveOptions
                    {
                        Format = ExcelSaveOptions.ExcelFormat.XLSX   // Export as .xlsx
                    };

                    // Save the single‑page document as an Excel file; tables on the page are exported
                    pageDoc.Save(outputPath, excelOpts);
                }
            }

            Console.WriteLine($"Tables from page 3 have been exported to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
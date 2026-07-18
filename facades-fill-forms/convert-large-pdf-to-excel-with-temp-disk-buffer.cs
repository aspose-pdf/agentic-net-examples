using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths (replace with actual file locations)
        const string inputPdfPath = "large_input.pdf";
        const string outputExcelPath = "output.xlsx";

        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "AsposePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the intermediate PDF that will be created using disk buffering
        string intermediatePdfPath = Path.Combine(tempFolder, "intermediate.pdf");

        try
        {
            // Load the source PDF to determine the total page count
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Generate an array of all page numbers (Aspose.Pdf uses 1‑based indexing)
                int[] allPages = Enumerable.Range(1, sourceDoc.Pages.Count).ToArray();

                // Use PdfFileEditor with disk buffering to write the intermediate PDF.
                // This avoids keeping the whole document in memory for large files.
                PdfFileEditor editor = new PdfFileEditor
                {
                    UseDiskBuffer = true // forces temporary disk usage for large operations
                };

                // Extract all pages to the intermediate file.
                // (You could extract a subset if needed.)
                editor.Extract(inputPdfPath, allPages, intermediatePdfPath);
            }

            // Load the intermediate PDF and convert it to Excel.
            using (Document intermediateDoc = new Document(intermediatePdfPath))
            {
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Minimize worksheets to reduce the number of generated sheets for large data
                    MinimizeTheNumberOfWorksheets = true
                };

                intermediateDoc.Save(outputExcelPath, excelOpts);
            }

            Console.WriteLine($"Excel file saved to '{outputExcelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder and its contents
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, recursive: true);
                }
            }
            catch
            {
                // If cleanup fails, ignore – the OS will eventually reclaim the temp files.
            }
        }
    }
}
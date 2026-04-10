using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the large PDF (generated from an Excel dataset elsewhere)
        const string sourcePdfPath = "large_input.pdf";

        // Desired final Excel output file
        const string outputExcelPath = "output.xlsx";

        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Use PdfFileEditor with disk buffering to avoid large memory usage
        PdfFileEditor editor = new PdfFileEditor
        {
            UseDiskBuffer = true // forces intermediate data to be written to disk
        };

        // Extract each page of the large PDF into separate temporary PDFs
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            int pageCount = srcDoc.Pages.Count; // 1‑based indexing
            for (int i = 1; i <= pageCount; i++)
            {
                string tempPdfPath = Path.Combine(tempFolder, $"page_{i}.pdf");
                editor.Extract(sourcePdfPath, new int[] { i }, tempPdfPath);
            }
        }

        // Concatenate the temporary PDFs back into a single PDF (optional step)
        // This demonstrates handling large files while keeping intermediate data on disk
        string[] tempPdfFiles = Directory.GetFiles(tempFolder, "page_*.pdf");
        string combinedPdfPath = Path.Combine(tempFolder, "combined.pdf");
        editor.Concatenate(tempPdfFiles, combinedPdfPath);

        // Convert the combined PDF to Excel, minimizing the number of worksheets
        ExcelSaveOptions excelOpts = new ExcelSaveOptions
        {
            MinimizeTheNumberOfWorksheets = true
        };

        using (Document combinedDoc = new Document(combinedPdfPath))
        {
            combinedDoc.Save(outputExcelPath, excelOpts);
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }

        Console.WriteLine($"Excel file successfully saved to '{outputExcelPath}'.");
    }
}
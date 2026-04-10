using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "template.pdf";          // PDF with multiple pages
        const string outputDirectory = "GeneratedPages";    // Folder for output files

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple placeholder
        // PDF with a few blank pages so the sample can run without external files.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            CreatePlaceholderPdf(inputPdfPath, 3);
            Console.WriteLine($"Placeholder PDF created at '{inputPdfPath}'.");
        }

        // Build a sample DataTable.
        // In a real scenario the DataTable would come from a database or other source.
        DataTable table = new DataTable("PageInfo");
        table.Columns.Add("PageNumber", typeof(int));   // 1‑based page index
        table.Columns.Add("FileName",   typeof(string)); // Desired file name without extension

        // Example rows
        table.Rows.Add(1, "Invoice_2023_001");
        table.Rows.Add(2, "Invoice_2023_002");
        table.Rows.Add(3, "SummaryReport");

        // Facade for page extraction
        PdfFileEditor editor = new PdfFileEditor();

        foreach (DataRow row in table.Rows)
        {
            // Use Convert.ToInt32 / Convert.ToString to avoid CS8600 warnings when the
            // underlying DataRow value could be DBNull.
            int pageNumber = Convert.ToInt32(row["PageNumber"]);
            string fileName = Convert.ToString(row["FileName"]) ?? "Untitled";

            // Build full output path: <outputDirectory>\<FileName>.pdf
            string outputPath = Path.Combine(outputDirectory, $"{fileName}.pdf");

            // Extract the specified page and save it as a separate PDF.
            // The Extract method creates a new PDF containing only the requested pages.
            editor.Extract(inputPdfPath, new int[] { pageNumber }, outputPath);
        }

        // No explicit Dispose needed for PdfFileEditor (it does not implement IDisposable).
        Console.WriteLine("Pages extracted with custom names.");
    }

    /// <summary>
    /// Creates a simple placeholder PDF with the requested number of blank pages.
    /// This helper is only used when the expected template file is missing.
    /// </summary>
    private static void CreatePlaceholderPdf(string path, int pageCount)
    {
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                // Add a blank page. You could add any content here if desired.
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }
}

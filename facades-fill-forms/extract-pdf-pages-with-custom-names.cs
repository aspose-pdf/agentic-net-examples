using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF that will be split
        const string inputPdf = "template.pdf";

        // Directory where the generated single‑page PDFs will be stored
        const string outputDir = "OutputPages";

        // --------------------------------------------------------------------
        // Prepare a DataTable that maps each page number to a custom file name.
        // In a real scenario this table could be filled from a database or other source.
        // --------------------------------------------------------------------
        DataTable namingTable = new DataTable();
        namingTable.Columns.Add("PageNumber", typeof(int));
        namingTable.Columns.Add("FileName", typeof(string));

        // Example rows – adjust as needed
        namingTable.Rows.Add(1, "Invoice_2023_01");
        namingTable.Rows.Add(2, "Invoice_2023_02");
        namingTable.Rows.Add(3, "SummaryReport");

        // Validate input PDF existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfFileEditor does not implement IDisposable, so a plain instance is sufficient
        PdfFileEditor editor = new PdfFileEditor();

        // Iterate over each mapping and extract the corresponding page
        foreach (DataRow row in namingTable.Rows)
        {
            int pageNumber = Convert.ToInt32(row["PageNumber"]); // 1‑based indexing
            string fileName = row["FileName"].ToString();

            // Build the full output file path (ensure .pdf extension)
            string outputPath = Path.Combine(outputDir, $"{fileName}.pdf");

            try
            {
                // Extract the specified page into a new PDF file
                editor.Extract(inputPdf, new int[] { pageNumber }, outputPath);
                Console.WriteLine($"Page {pageNumber} saved as '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to extract page {pageNumber}: {ex.Message}");
            }
        }

        // No explicit Close() is required for PdfFileEditor in this context
    }
}

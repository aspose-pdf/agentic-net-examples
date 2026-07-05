using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF that will be split into separate pages.
        const string inputPdfPath = "source.pdf";

        // Directory where the generated PDF pages will be saved.
        const string outputDirectory = "GeneratedPages";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists. If it does not, create a simple placeholder
        // PDF with as many pages as we have rows in the naming table. This prevents
        // the FileNotFoundException that caused the original crash.
        // ---------------------------------------------------------------------
        // Example DataTable containing custom naming information.
        DataTable namingTable = new DataTable();
        namingTable.Columns.Add("PageNumber", typeof(int));
        namingTable.Columns.Add("FileName", typeof(string));
        namingTable.Rows.Add(1, "Introduction");
        namingTable.Rows.Add(2, "Chapter1");
        namingTable.Rows.Add(3, "Chapter2");
        namingTable.Rows.Add(4, "Conclusion");

        // Create a dummy source PDF if it does not already exist.
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a page for each row in the naming table.
                foreach (DataRow row in namingTable.Rows)
                {
                    int pageIdx = (int)row["PageNumber"];
                    Page page = placeholder.Pages.Add();
                    // Simple content so we can see which page is which.
                    TextFragment tf = new TextFragment($"This is page {pageIdx}");
                    tf.Position = new Position(100, 700);
                    page.Paragraphs.Add(tf);
                }
                placeholder.Save(inputPdfPath);
            }
        }

        // PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using statement.
        PdfFileEditor editor = new PdfFileEditor();
        foreach (DataRow row in namingTable.Rows)
        {
            int pageNumber = (int)row["PageNumber"];
            string customName = (string)row["FileName"];

            // Build the full output path for the extracted page.
            string outputPath = Path.Combine(outputDirectory, $"{customName}.pdf");

            // Extract the specified page and save it with the custom file name.
            // The Extract method expects an array of page numbers (1‑based indexing).
            editor.Extract(inputPdfPath, new int[] { pageNumber }, outputPath);
        }

        Console.WriteLine("Pages have been extracted with custom names.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_split_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Create a table and disable automatic page splitting
                Table table = new Table
                {
                    // Prevent the table from breaking across pages
                    IsBroken = false,
                    // Define column widths (optional, adjust as needed)
                    ColumnWidths = "100 100 100"
                };

                // Add a header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");
                header.Cells.Add("Header 3");

                // Add enough rows to force a large table
                for (int i = 1; i <= 20; i++)
                {
                    Row row = table.Rows.Add();
                    row.Cells.Add($"Row {i} Col 1");
                    row.Cells.Add($"Row {i} Col 2");
                    row.Cells.Add($"Row {i} Col 3");
                }

                // Insert the table into the first page (1‑based indexing)
                Page page = doc.Pages[1];
                page.Paragraphs.Add(table);

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'. Table will stay on one page.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
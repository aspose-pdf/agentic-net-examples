using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // Create a new table with two columns
            // -------------------------------------------------
            Table newTable = new Table
            {
                // Define column widths (percentage or absolute units)
                ColumnWidths = "200 200",
                // Optional: set a default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a row with two cells
            Row row = newTable.Rows.Add();
            row.Cells.Add("First cell content");
            row.Cells.Add("Second cell content");

            // -------------------------------------------------
            // Replace the first existing table on page 14
            // -------------------------------------------------
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 14)
            {
                Console.Error.WriteLine("The document has fewer than 14 pages.");
                return;
            }

            Page page14 = doc.Pages[14];

            // Find the first Table object in the page's Paragraphs collection
            bool replaced = false;
            for (int i = 0; i < page14.Paragraphs.Count; i++)
            {
                if (page14.Paragraphs[i] is Table)
                {
                    // Replace the existing table with the new one
                    page14.Paragraphs[i] = newTable;
                    replaced = true;
                    break;
                }
            }

            // If no table was found, optionally add the new table at the end
            if (!replaced)
            {
                page14.Paragraphs.Add(newTable);
            }

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
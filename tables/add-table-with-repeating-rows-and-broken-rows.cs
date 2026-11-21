using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table to.
            Page page = doc.Pages[1]; // 1‑based indexing

            // Create a table and configure its properties.
            Table table = new Table
            {
                // The first two rows will repeat on each new page.
                RepeatingRowsCount = 2,

                // Allow rows to be split vertically across pages.
                IsBroken = true
            };

            // Define column widths (optional, adjust as needed).
            table.ColumnWidths = "100 200 100";

            // ----- Header rows (will repeat) -----
            Row headerRow1 = table.Rows.Add();
            headerRow1.Cells.Add("Header 1");
            headerRow1.Cells.Add("Header 2");
            headerRow1.Cells.Add("Header 3");

            Row headerRow2 = table.Rows.Add();
            headerRow2.Cells.Add("Subheader A");
            headerRow2.Cells.Add("Subheader B");
            headerRow2.Cells.Add("Subheader C");

            // ----- Data rows -----
            for (int i = 1; i <= 30; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Item {i}");
                dataRow.Cells.Add($"Description of item {i}");
                dataRow.Cells.Add($"{i * 10}");
            }

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}

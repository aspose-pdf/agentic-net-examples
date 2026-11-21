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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page; add a new page if the document is empty
            Page page;
            if (doc.Pages.Count == 0)
                page = doc.Pages.Add();
            else
                page = doc.Pages[1]; // 1‑based indexing

            // Create a table using the core Table API
            Table table = new Table
            {
                // Define column widths (5 columns in this example)
                ColumnWidths = "100 100 100 100 100",
                // Repeat the first 3 columns on each page fragment
                RepeatingColumnsCount = 3,
                // Allow vertical breaks within the same page
                IsBroken = true,
                // Optional visual border for the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Build header row
            Row headerRow = table.Rows.Add();
            for (int i = 1; i <= 5; i++)
            {
                Cell headerCell = headerRow.Cells.Add();
                headerCell.Paragraphs.Add(new TextFragment($"Header {i}"));
                // Optional styling for header cells can be added here
            }

            // Build data rows (20 rows in this example)
            for (int r = 1; r <= 20; r++)
            {
                Row dataRow = table.Rows.Add();
                for (int c = 1; c <= 5; c++)
                {
                    Cell dataCell = dataRow.Cells.Add();
                    dataCell.Paragraphs.Add(new TextFragment($"R{r}C{c}"));
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}

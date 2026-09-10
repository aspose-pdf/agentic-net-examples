using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";
        const int    targetPage = 2;               // 1‑based page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (targetPage < 1 || targetPage > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Access the specific page (page indexing is 1‑based)
            Page page = doc.Pages[targetPage];

            // Create a table
            Table table = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                Alignment = HorizontalAlignment.Center
            };

            // Define column widths (percentage of the page width)
            table.ColumnWidths = "50 50";

            // ----- Header row -----
            Row header = table.Rows.Add();
            // First header cell
            Cell cell1 = header.Cells.Add();
            cell1.BackgroundColor = Color.LightGray;
            cell1.Paragraphs.Add(new TextFragment("Product"));
            // Second header cell
            Cell cell2 = header.Cells.Add();
            cell2.BackgroundColor = Color.LightGray;
            cell2.Paragraphs.Add(new TextFragment("Price"));

            // ----- Data rows -----
            // Row 1
            Row row1 = table.Rows.Add();
            row1.Cells.Add().Paragraphs.Add(new TextFragment("Widget A"));
            row1.Cells.Add().Paragraphs.Add(new TextFragment("$10.00"));

            // Row 2
            Row row2 = table.Rows.Add();
            row2.Cells.Add().Paragraphs.Add(new TextFragment("Widget B"));
            row2.Cells.Add().Paragraphs.Add(new TextFragment("$15.50"));

            // Position the table on the page (optional)
            // The Table inherits from BaseParagraph, so we can set its position via the MarginInfo
            // Here we place it 100 points from the top of the page.
            table.Margin = new MarginInfo(0, 0, 100, 0); // Left, Right, Top, Bottom

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPage}. Saved as '{outputPath}'.");
    }
}
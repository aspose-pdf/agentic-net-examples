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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define page margins in points
        const double topMargin = 50;
        const double bottomMargin = 50;
        const double sideMargin = 50; // left and right margin

        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Total page height
            double pageHeight = page.PageInfo.Height;

            // Approximate existing content height by summing font sizes of text fragments
            double existingContentHeight = 0;
            foreach (BaseParagraph para in page.Paragraphs)
            {
                if (para is TextFragment tf && tf.TextState != null)
                {
                    existingContentHeight += tf.TextState.FontSize;
                }
            }

            // Remaining vertical space after accounting for margins and existing content
            double remainingSpace = pageHeight - topMargin - bottomMargin - existingContentHeight;
            if (remainingSpace < 0) remainingSpace = 0;

            // Create a table that fits within the remaining space
            Table table = new Table
            {
                // Set side margins for the table
                Margin = new MarginInfo(sideMargin, bottomMargin, sideMargin, 0),
                // Optional: you can also set the table's width explicitly if needed
                // Width = page.PageInfo.Width - 2 * sideMargin
            };
            table.ColumnWidths = "200 200";

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell 1");
            data.Cells.Add("Cell 2");

            // Adjust row heights so the table does not exceed the remaining space
            // Distribute the available height equally among all rows
            int rowCount = table.Rows.Count;
            if (rowCount > 0 && remainingSpace > 0)
            {
                float rowHeight = (float)(remainingSpace / rowCount);
                foreach (Row r in table.Rows)
                {
                    r.FixedRowHeight = rowHeight;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}

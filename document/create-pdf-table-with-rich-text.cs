using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rich_text_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a page (Pages[1] is the first page – 1‑based indexing).
            Page page = doc.Pages.Add();

            // Create a table and set its column widths (percent of page width).
            Table table = new Table
            {
                ColumnWidths = "200 200 200", // three equal columns
                Border = new BorderInfo(BorderSide.All, 0.5f) // thin border for visibility
            };

            // Row 1 – Header cells with bold text.
            Row headerRow = table.Rows.Add();
            AddCellWithRichText(headerRow, "Product", true, false, Aspose.Pdf.Color.LightGray);
            AddCellWithRichText(headerRow, "Quantity", true, false, Aspose.Pdf.Color.LightGray);
            AddCellWithRichText(headerRow, "Price", true, false, Aspose.Pdf.Color.LightGray);

            // Row 2 – Data cells with mixed formatting.
            Row dataRow1 = table.Rows.Add();
            AddCellWithRichText(dataRow1, "Widget A", false, false, Aspose.Pdf.Color.Black);
            AddCellWithRichText(dataRow1, "10", false, true, Aspose.Pdf.Color.Blue);
            AddCellWithRichText(dataRow1, "$15.00", false, false, Aspose.Pdf.Color.DarkGreen);

            // Row 3 – Data cells with italic and colored text.
            Row dataRow2 = table.Rows.Add();
            AddCellWithRichText(dataRow2, "Gadget B", false, true, Aspose.Pdf.Color.DarkRed);
            AddCellWithRichText(dataRow2, "5", false, false, Aspose.Pdf.Color.Black);
            AddCellWithRichText(dataRow2, "$42.50", true, false, Aspose.Pdf.Color.Purple);

            // Add the table to the page's paragraphs collection.
            page.Paragraphs.Add(table);

            // Save the PDF. No SaveOptions needed because the target format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }

    // Helper method to create a cell containing a TextFragment with rich formatting.
    static void AddCellWithRichText(Row row, string text, bool bold, bool italic, Aspose.Pdf.Color foreColor)
    {
        // Create a TextFragment for the cell content.
        TextFragment tf = new TextFragment(text);

        // Apply rich text formatting via TextState.
        tf.TextState.FontSize = 12;
        // FontStyle does not have a 'None' member; use a cast of 0 for "no style".
        tf.TextState.FontStyle = (bold ? FontStyles.Bold : (FontStyles)0) |
                                 (italic ? FontStyles.Italic : (FontStyles)0);
        tf.TextState.ForegroundColor = foreColor;

        // Add the TextFragment to a new cell and then to the row.
        Cell cell = row.Cells.Add();
        cell.Paragraphs.Add(tf);
    }
}

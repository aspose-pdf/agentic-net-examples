using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "cell_alignment.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a new page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a simple table with one column
            Table table = new Table();
            table.ColumnWidths = "200"; // width of the single column

            // Add a row to the table
            Row row = table.Rows.Add();

            // Create a cell, add some text, and set vertical alignment to middle (center)
            Cell cell = new Cell();
            cell.Paragraphs.Add(new TextFragment("Centered Text"));
            cell.VerticalAlignment = VerticalAlignment.Center; // Middle alignment

            // Add the cell to the row
            row.Cells.Add(cell);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (saving to a .pdf path writes PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo and BorderSide

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithBorder.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its column widths
            Table table = new Table
            {
                // Widths: 100, 200, 200 points respectively
                ColumnWidths = "100 200 200"
            };

            // Apply a solid border to the entire table
            // BorderInfo defines the style, width and (optionally) color of the border
            BorderInfo solidBorder = new BorderInfo(BorderSide.All, 1f); // 1‑point solid line on all sides
            table.Border = solidBorder; // Set the Border property (Table has no BorderInfo property)

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Description");

            // Add a sample data row
            Row data = table.Rows.Add();
            data.Cells.Add("1");
            data.Cells.Add("Sample Item");
            data.Cells.Add("This is a description.");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with bordered table saved to '{outputPath}'.");
    }
}

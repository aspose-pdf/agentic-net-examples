using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_page.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns; set column widths and a border
            Table table = new Table
            {
                ColumnWidths = "100 150 200",                     // column widths in points
                Border = new BorderInfo(BorderSide.All, 0.5f)    // thin border around the table
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray; // use Aspose.Pdf.Color to avoid System.Drawing
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // ----- First data row -----
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("10");
            row1.Cells.Add("$5.00");

            // ----- Second data row -----
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("7");
            row2.Cells.Add("$7.50");

            // Add the table to the page's content (Paragraphs collection)
            page.Paragraphs.Add(table);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
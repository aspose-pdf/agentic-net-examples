using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_page.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a new blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Define three column widths (in points)
                ColumnWidths = "100 150 200",
                // Apply a thin black border to all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                // Add padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            Cell h1 = header.Cells.Add();
            h1.BackgroundColor = Color.LightGray;
            h1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell h2 = header.Cells.Add();
            h2.BackgroundColor = Color.LightGray;
            h2.Paragraphs.Add(new TextFragment("Header 2"));
            Cell h3 = header.Cells.Add();
            h3.BackgroundColor = Color.LightGray;
            h3.Paragraphs.Add(new TextFragment("Header 3"));

            // ----- Data row -----
            Row data = table.Rows.Add();
            data.Cells.Add().Paragraphs.Add(new TextFragment("Row 1, Col 1"));
            data.Cells.Add().Paragraphs.Add(new TextFragment("Row 1, Col 2"));
            data.Cells.Add().Paragraphs.Add(new TextFragment("Row 1, Col 3"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
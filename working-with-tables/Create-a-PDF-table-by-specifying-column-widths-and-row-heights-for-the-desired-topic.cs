using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string outputPath = "table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the table
            Page page = doc.Pages.Add();

            // Instantiate the table
            Table table = new Table();

            // Define column widths (points). Example: three columns with widths 100, 150, and 200
            table.ColumnWidths = "100 150 200";

            // Optional: set a simple border for visual clarity
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // ----- Row 1 (e.g., header) -----
            Row row1 = table.Rows.Add();
            row1.FixedRowHeight = 30; // fixed height in points
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");
            row1.Cells.Add("Header 3");

            // ----- Row 2 -----
            Row row2 = table.Rows.Add();
            row2.FixedRowHeight = 50;
            row2.Cells.Add("Cell 1");
            row2.Cells.Add("Cell 2");
            row2.Cells.Add("Cell 3");

            // ----- Row 3 -----
            Row row3 = table.Rows.Add();
            row3.FixedRowHeight = 40;
            row3.Cells.Add("Data A");
            row3.Cells.Add("Data B");
            row3.Cells.Add("Data C");

            // Add the fully constructed table to the page
            page.Paragraphs.Add(table);

            // Save the document as PDF (no SaveOptions needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
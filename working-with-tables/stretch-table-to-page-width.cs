using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_autofit.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a table and stretch it across the page width
            Table table = new Table
            {
                // Use ColumnAdjustment.AutoFitToWindow to make the table fill the page width
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add(CreateCell("Header 1"));
            header.Cells.Add(CreateCell("Header 2"));
            header.Cells.Add(CreateCell("Header 3"));

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add(CreateCell("Cell 1"));
            data.Cells.Add(CreateCell("Cell 2"));
            data.Cells.Add(CreateCell("Cell 3"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to create a table cell containing a text fragment
    private static Cell CreateCell(string text)
    {
        TextFragment tf = new TextFragment(text);
        return new Cell { Paragraphs = { tf } };
    }
}

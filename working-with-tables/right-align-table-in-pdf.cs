using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "right_aligned_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page where the table will be placed
            Page page = doc.Pages[1];

            // Create a new table (default constructor)
            Table table = new Table();

            // Define two columns with equal widths – ColumnWidths is a string, not a collection
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            AddCell(header, "Header 1");
            AddCell(header, "Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            AddCell(data, "Value 1");
            AddCell(data, "Value 2");

            // Align the table to the right margin
            table.HorizontalAlignment = HorizontalAlignment.Right;

            // Adjust the left margin (distance from the left page edge)
            // Since the table is right‑aligned, setting Left shifts it leftwards.
            // Here we set a 30‑point left margin as an example.
            table.Left = 30;

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to add a text cell to a row
    private static void AddCell(Row row, string text)
    {
        Cell cell = new Cell();
        cell.Paragraphs.Add(new TextFragment(text));
        row.Cells.Add(cell);
    }
}

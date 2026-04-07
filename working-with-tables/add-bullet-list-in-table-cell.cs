using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "list_in_cell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set column widths (single column in this example)
                ColumnWidths = "200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a list of items as separate paragraphs.
            // Each paragraph starts with a bullet character to represent a list item.
            string[] items = { "First item", "Second item", "Third item" };
            foreach (string item in items)
            {
                // Create a text fragment with a bullet prefix
                TextFragment tf = new TextFragment("• " + item);
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment as a paragraph to the cell
                cell.Paragraphs.Add(tf);
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list inside a cell saved to '{outputPath}'.");
    }
}
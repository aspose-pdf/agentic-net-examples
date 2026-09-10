using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths (adjust as needed)
        const string outputPath = "ListInCell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with one row and one column
            Table table = new Table
            {
                // Position and size of the table on the page
                ColumnWidths = "200", // single column width
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Optional: set cell padding and background
            cell.Margin = new MarginInfo(5, 5, 5, 5);
            cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Create list items as paragraphs with bullet markers
            // Using Unicode bullet character (U+2022) as the list marker
            string[] items = { "First item", "Second item", "Third item" };
            foreach (string item in items)
            {
                // Each list item is a separate paragraph inside the cell
                TextFragment tf = new TextFragment("\u2022 " + item);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the paragraph (TextFragment) to the cell's Paragraphs collection
                cell.Paragraphs.Add(tf);
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list in a cell saved to '{outputPath}'.");
    }
}
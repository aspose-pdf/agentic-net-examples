using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "list_in_cell.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Single column of width 200 points
                ColumnWidths = "200",
                // Simple black border around the table
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a single row to the table
            Row row = table.Rows.Add();

            // Add a single cell to the row
            Cell cell = row.Cells.Add();

            // Define the list items to be placed inside the cell
            string[] listItems = { "First item", "Second item", "Third item" };

            // For each item, create a paragraph that starts with a bullet marker
            foreach (string item in listItems)
            {
                // Create a TextFragment containing a bullet (U+2022) and the item text
                TextFragment tf = new TextFragment("\u2022 " + item);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the TextFragment to the cell's Paragraphs collection
                cell.Paragraphs.Add(tf);
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list inside a cell saved to '{outputPath}'.");
    }
}
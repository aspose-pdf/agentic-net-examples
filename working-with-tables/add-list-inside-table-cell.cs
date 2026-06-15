using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create a sample PDF file (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a bullet list inside a cell
        using (Document doc = new Document("input.pdf"))
        {
            // Create a table with two columns
            Table table = new Table();
            doc.Pages[1].Paragraphs.Add(table);
            table.ColumnWidths = "250 250";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell where the list will be placed
            Cell cell = row.Cells.Add();

            // Define list items (maximum 4 to respect evaluation limits)
            string[] listItems = new string[] { "First item", "Second item", "Third item" };

            // Insert each item as a paragraph prefixed with a bullet character
            foreach (string item in listItems)
            {
                TextFragment tf = new TextFragment("\u2022 " + item);
                tf.TextState.Font = FontRepository.FindFont("Times New Roman");
                tf.TextState.FontSize = 12;
                tf.TextState.ForegroundColor = Color.Black;
                cell.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save("output.pdf");
        }
    }
}
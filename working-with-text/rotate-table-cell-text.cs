using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class RotateTextInTableCell
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a new page for the table (or use an existing page)
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 1f),
                Margin = new MarginInfo(10, 10, 10, 10)
            };
            page.Paragraphs.Add(table);

            // Define column width(s) – here a single column of 200 points
            table.ColumnWidths = "200"; // or new float[] {200}

            // Create a single row and set its height
            Row row = table.Rows.Add();
            row.FixedRowHeight = 50; // height of the row (and thus the cell)

            // Create a cell in the row
            Cell cell = row.Cells.Add();

            // Create a TextFragment and rotate it by 60 degrees using TextState.Rotation
            TextFragment tf = new TextFragment("Rotated Text");
            // The Rotation property belongs to TextState, not TextFragment itself.
            tf.TextState.Rotation = 60; // degrees clockwise

            // Add the rotated TextFragment to the cell's paragraph collection
            cell.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rotated text in table cell: {outputPath}");
    }
}

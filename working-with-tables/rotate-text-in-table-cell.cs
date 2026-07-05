using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "rotated_cell_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set table width or other properties here
                ColumnWidths = "200 200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a TextFragment with the desired text
            TextFragment tf = new TextFragment("Rotated Text");

            // Set the rotation angle (in degrees) on the TextFragment's TextState
            tf.TextState.Rotation = 45; // rotate 45 degrees

            // Add the TextFragment to the cell's paragraphs collection
            cell.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
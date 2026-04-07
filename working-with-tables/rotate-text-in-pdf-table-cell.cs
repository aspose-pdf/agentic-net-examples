using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "rotated_cell.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column of width 200 points
            Table table = new Table { ColumnWidths = "200" };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Rotated Text");

            // Rotate the text inside the cell by setting the rotation (in degrees)
            fragment.TextState.Rotation = 45; // rotate 45 degrees

            // Add the rotated text fragment to the cell's paragraphs collection
            cell.Paragraphs.Add(fragment);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rotated cell text saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table with two columns (adjust widths as needed)
            Table table = new Table
            {
                ColumnWidths = "200 200"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create first text fragment
            TextFragment tf1 = new TextFragment("First line of text");
            tf1.TextState.FontSize = 12;
            tf1.TextState.Font = FontRepository.FindFont("Helvetica");
            tf1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Create a line‑break fragment (a TextFragment containing a newline)
            TextFragment lineBreak = new TextFragment("\n");

            // Create second text fragment
            TextFragment tf2 = new TextFragment("Second line of text");
            tf2.TextState.FontSize = 12;
            tf2.TextState.Font = FontRepository.FindFont("Helvetica");
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragments to the cell's Paragraphs collection in order
            cell.Paragraphs.Add(tf1);
            cell.Paragraphs.Add(lineBreak); // inserts a line break
            cell.Paragraphs.Add(tf2);

            // Save the PDF (using the lifecycle rule for disposal)
            doc.Save("MultilineCell.pdf");
        }

        Console.WriteLine("PDF with multiline cell created successfully.");
    }
}
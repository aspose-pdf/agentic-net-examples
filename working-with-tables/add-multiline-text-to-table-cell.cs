using System;
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

            // Create a table and add it to the page
            Table table = new Table
            {
                // Optional: set table border for visibility
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Set cell properties (optional)
            cell.BackgroundColor = Color.LightGray;
            cell.IsWordWrapped = true;

            // Create first line of text
            TextFragment line1 = new TextFragment("First line of text");
            // Create a line‑break fragment (empty text with a newline)
            TextFragment lineBreak = new TextFragment(Environment.NewLine);
            // Create second line of text
            TextFragment line2 = new TextFragment("Second line of text");
            // Create third line of text
            TextFragment line3 = new TextFragment("Third line of text");

            // Add the fragments to the cell's Paragraphs collection.
            // The line‑break fragment separates the lines.
            cell.Paragraphs.Add(line1);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line2);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line3);

            // Save the document
            doc.Save("MultilineCell.pdf");
        }

        Console.WriteLine("PDF with multiline cell created successfully.");
    }
}
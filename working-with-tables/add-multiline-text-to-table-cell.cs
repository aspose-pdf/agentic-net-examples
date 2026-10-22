using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multiline_cell.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (var doc = new Document())
        {
            // Add a new page (first page)
            var page = doc.Pages.Add();

            // Create a table with a single column of width 300 points
            var table = new Table
            {
                ColumnWidths = "300"
            };

            // Add a row to the table
            var row = table.Rows.Add();

            // Add a cell to the row
            var cell = row.Cells.Add();

            // First line of text
            cell.Paragraphs.Add(new TextFragment("This is the first line."));
            // Insert a line break fragment
            cell.Paragraphs.Add(new TextFragment("\n"));
            // Second line of text
            cell.Paragraphs.Add(new TextFragment("This is the second line."));
            // Insert another line break fragment
            cell.Paragraphs.Add(new TextFragment("\n"));
            // Third line of text
            cell.Paragraphs.Add(new TextFragment("Third line goes here."));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiline cell saved to '{outputPath}'.");
    }
}

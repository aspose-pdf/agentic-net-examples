using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextFragment used in table cells

class Program
{
    static void Main()
    {
        // Input and output paths (adjust as needed)
        const string outputPath = "table_page.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table
            {
                // Optional: set table position and size
                // Left and Top define the upper‑left corner of the table on the page
                Left = 50,
                Top = 700,
                // Set a border for visual clarity
                Border = new BorderInfo(BorderSide.All, 1, Color.Black)
            };

            // Define column widths (optional)
            // Here we create two columns: 200 and 200 points wide
            table.ColumnWidths = "200 200";

            // Add first row (header)
            Row headerRow = table.Rows.Add();
            // Header cell 1
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            // Header cell 2
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.BackgroundColor = Color.LightGray;
            headerCell2.Paragraphs.Add(new TextFragment("Price"));

            // Add a second row (data)
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Widget A"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("$49.99"));

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
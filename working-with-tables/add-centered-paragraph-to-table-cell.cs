using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input/Output paths
        const string outputPath = "table_with_centered_paragraph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                Left = 50,
                Top = 700,
                // Define column widths (two equal columns)
                ColumnWidths = "200 200"
            };

            // Add a row to the table
            var row = table.Rows.Add();

            // Add a cell to the row
            var cell = row.Cells.Add();

            // Create a text fragment that will be placed inside the cell
            TextFragment paragraph = new TextFragment("Centered paragraph inside cell")
            {
                // Center the text horizontally within the cell
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Add the paragraph to the cell's paragraph collection
            cell.Paragraphs.Add(paragraph);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
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
                // Optional: set table width
                ColumnWidths = "200"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Centered paragraph inside cell")
            {
                // Center the text horizontally within the cell
                HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center
            };

            // Add the text fragment to the cell's paragraph collection
            cell.Paragraphs.Add(tf);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
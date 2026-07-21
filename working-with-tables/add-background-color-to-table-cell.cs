using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text fragments if needed

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableWithColoredCell.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 2 columns and 2 rows
            Table table = new Table
            {
                // Optional: set overall table border and margin
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                Margin = new MarginInfo(10, 10, 10, 10)
            };

            // First row
            Row headerRow = table.Rows.Add();
            // Header cell 1
            Cell headerCell1 = headerRow.Cells.Add("Header 1");
            // Header cell 2
            Cell headerCell2 = headerRow.Cells.Add("Header 2");

            // Second row
            Row dataRow = table.Rows.Add();
            // Data cell 1 (will have a background color)
            Cell dataCell1 = dataRow.Cells.Add("Data A");
            // Set the background color of this cell
            dataCell1.BackgroundColor = Aspose.Pdf.Color.LightGray; // desired color

            // Data cell 2 (normal)
            Cell dataCell2 = dataRow.Cells.Add("Data B");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
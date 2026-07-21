using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Set the page size to landscape (swap width/height of A4: 842 x 595 points)
            page.SetPageSize(842, 595);

            // Explicitly mark the page as landscape (optional but clarifies intent)
            page.PageInfo.IsLandscape = true;

            // Create a wide table that will benefit from the landscape orientation
            Table table = new Table
            {
                // Define column widths that together exceed typical portrait width
                ColumnWidths = "100 100 100 100 100",
                // Add a thin black border around each cell
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Add padding inside cells for readability
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                // Center the table horizontally on the page
                Alignment = HorizontalAlignment.Center
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            header.Cells.Add("Header 4");
            header.Cells.Add("Header 5");

            // Sample data rows
            for (int i = 0; i < 10; i++)
            {
                Row row = table.Rows.Add();
                for (int j = 1; j <= 5; j++)
                {
                    row.Cells.Add($"R{i + 1}C{j}");
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
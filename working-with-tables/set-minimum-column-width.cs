using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_min_width.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 3 columns
            Table table = new Table
            {
                // Set the default column width to 50 points.
                // This acts as a minimum width for any column that does not have an explicit width.
                DefaultColumnWidth = "50",

                // Optionally define explicit widths for each column.
                // Values are in points; you can mix units (e.g., "50 2cm 1in").
                ColumnWidths = "50 70 90",

                // Add a thin border so the table is visible.
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Populate the table with sample data
            for (int i = 0; i < 5; i++)
            {
                Row row = table.Rows.Add();
                for (int j = 0; j < 3; j++)
                {
                    // Each cell contains simple text.
                    Cell cell = row.Cells.Add($"R{i + 1}C{j + 1}");
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
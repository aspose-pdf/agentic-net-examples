using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for Table and MarginInfo

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_padding.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Table and set default cell padding for all cells
            Table table = new Table
            {
                // MarginInfo defines padding on each side of a cell
                DefaultCellPadding = new MarginInfo
                {
                    Left   = 5,   // 5 points left padding
                    Right  = 5,   // 5 points right padding
                    Top    = 2,   // 2 points top padding
                    Bottom = 2    // 2 points bottom padding
                },
                // Optional: set column widths (percentage of page width)
                ColumnWidths = "30% 70%"
            };

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Item");
            Cell headerCell2 = header.Cells.Add("Description");
            // Header cells can have their own style if needed
            headerCell1.BackgroundColor = Color.LightGray;
            headerCell2.BackgroundColor = Color.LightGray;

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Apple");
            data.Cells.Add("A sweet red fruit.");

            // Add another data row
            Row data2 = table.Rows.Add();
            data2.Cells.Add("Banana");
            data2.Cells.Add("A long yellow fruit.");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (can be empty)
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and set its overall border
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top  = 500,
                // Optional: set table width by column widths (3 equal columns)
                ColumnWidths = "150 150 150",
                // Table border (use BorderInfo constructor – no Color/Width setters)
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black),
                // Default cell border
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Gray)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.LightBlue;
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add data rows with alternating background colors
            for (int i = 0; i < 6; i++)
            {
                Row row = table.Rows.Add();
                // Alternate row colors
                row.BackgroundColor = (i % 2 == 0) ? Color.LightGray : Color.White;

                row.Cells.Add($"Row {i + 1} - Col 1");
                row.Cells.Add($"Row {i + 1} - Col 2");
                row.Cells.Add($"Row {i + 1} - Col 3");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}

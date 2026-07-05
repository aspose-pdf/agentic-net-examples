using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document (lifecycle: create)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set default styling for all cells
            Table table = new Table
            {
                // Define three equal-width columns
                ColumnWidths = "100 100 100",

                // Default padding for cells (applies if a cell does not override)
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 },

                // Default border for cells (applies if a cell does not override)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),

                // Table border and background (optional visual aid)
                Border = new BorderInfo(BorderSide.All, 1f, Color.DarkGray),
                BackgroundColor = Color.LightGray
            };

            // Populate the table with rows and cells
            for (int r = 0; r < 3; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 3; c++)
                {
                    // Add a cell with sample text
                    Cell cell = row.Cells.Add($"R{r + 1}C{c + 1}");

                    // Style each cell individually for accessibility
                    cell.BackgroundColor = Color.FromRgb(0.9, 0.9, 1); // light blue background
                    cell.Border = new BorderInfo(BorderSide.All, 1.0f, Color.Blue); // thicker border for better visibility
                    cell.Margin = new MarginInfo { Top = 4, Bottom = 4, Left = 4, Right = 4 }; // padding
                }
            }

            // Add the styled table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}

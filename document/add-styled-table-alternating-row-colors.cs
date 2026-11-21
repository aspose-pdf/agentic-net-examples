using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for Color, BorderInfo, BorderSide
using Aspose.Pdf.Text;      // for TextState

class Program
{
    static void Main()
    {
        // Input PDF (can be an existing file or a new blank PDF)
        const string inputPath  = "input.pdf";
        // Output PDF with the styled table
        const string outputPath = "output_with_table.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();               // add a single empty page
                blank.Save(inputPath);           // save the placeholder file
            }
        }

        // Load the PDF, modify it, and save – all within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with three columns of equal width
            Table table = new Table
            {
                ColumnWidths = "150 150 150",                     // column widths in points
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black) // outer border for the table with color
            };

            // ---------- Header Row ----------
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray; // header background

            // Add three header cells
            Cell h1 = header.Cells.Add("Product");
            Cell h2 = header.Cells.Add("Quantity");
            Cell h3 = header.Cells.Add("Price");

            // Apply cell borders and text alignment
            foreach (Cell cell in header.Cells)
            {
                cell.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Aspose.Pdf.Color.Black,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
            }

            // ---------- Data Rows ----------
            // Sample data
            string[,] data = {
                { "Widget A", "10", "$5.00" },
                { "Widget B", "7",  "$7.50" },
                { "Widget C", "15", "$3.20" },
                { "Widget D", "3",  "$12.00" }
            };

            // Add rows with alternating background colors
            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row row = table.Rows.Add();

                // Alternate row color: even rows LightYellow, odd rows White
                row.BackgroundColor = (i % 2 == 0)
                    ? Aspose.Pdf.Color.FromRgb(1.0, 1.0, 0.9)   // LightYellow
                    : Aspose.Pdf.Color.White;

                // Populate cells
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell cell = row.Cells.Add(data[i, j]);

                    // Apply cell borders with color
                    cell.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black);

                    // Text formatting for data cells
                    cell.DefaultCellTextState = new TextState
                    {
                        FontSize = 11,
                        ForegroundColor = Aspose.Pdf.Color.Black,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}

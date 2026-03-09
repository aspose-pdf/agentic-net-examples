using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Text;                // TextFragment, TextState, etc.

class InsertTableExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be inserted (e.g., first page)
            Page page = doc.Pages[1];   // 1‑based indexing

            // ------------------------------------------------------------
            // Create a new Table and configure its appearance
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Position the table on the page (coordinates in points)
                // Left = 50, Top = 600 places the table near the top‑left corner
                Left = 50,
                Top  = 600,

                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // ------------------------------------------------------------
            // Define column widths (relative percentages of the table width)
            // ------------------------------------------------------------
            table.ColumnWidths = "30 40 30"; // three columns: 30%, 40%, 30%

            // ------------------------------------------------------------
            // Add rows and cells with text
            // ------------------------------------------------------------
            // Header row
            Row header = table.Rows.Add();
            Cell cell1 = header.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Product"));
            Cell cell2 = header.Cells.Add();
            cell2.Paragraphs.Add(new TextFragment("Quantity"));
            Cell cell3 = header.Cells.Add();
            cell3.Paragraphs.Add(new TextFragment("Price"));

            // Sample data rows
            string[,] data = {
                { "Widget A", "15", "$12.99" },
                { "Widget B", "8",  "$9.50"  },
                { "Widget C", "23", "$7.25"  }
            };

            for (int r = 0; r < data.GetLength(0); r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < data.GetLength(1); c++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment(data[r, c]));
                }
            }

            // ------------------------------------------------------------
            // Insert the table into the page's content stream
            // ------------------------------------------------------------
            page.Paragraphs.Add(table);

            // Save the modified PDF (still using the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
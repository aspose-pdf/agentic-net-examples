using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_borders_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three columns of equal width
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellTextState = new TextState { FontSize = 12 }
            };
            page.Paragraphs.Add(table);

            // Populate the table with 6 rows (including a header row)
            for (int i = 0; i < 6; i++)
            {
                Row row = table.Rows.Add();

                // Add three cells to each row
                for (int j = 0; j < 3; j++)
                {
                    Cell cell = new Cell();
                    cell.Paragraphs.Add(new TextFragment($"R{i + 1}C{j + 1}"));
                    row.Cells.Add(cell);
                }

                // Apply custom border style based on row index (1‑based)
                // Even rows: thick red border
                // Odd rows: thin blue border
                if ((i + 1) % 2 == 0) // even row
                {
                    // Set a thick red border for the entire row
                    row.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.Red);
                }
                else // odd row
                {
                    // Set a thin blue border for the entire row
                    row.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Blue);
                }
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom table borders saved to '{outputPath}'.");
    }
}

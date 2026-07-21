using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF (can be empty or existing)
        const string outputPdf = "output.pdf";  // result PDF with colored table

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a new page if the document has no pages
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Example column widths (percentage of page width)
                ColumnWidths = "33 33 34",
                // Optional: set table border
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Populate the table with sample data (5 rows)
            for (int r = 0; r < 5; r++)
            {
                Row row = table.Rows.Add(); // Add a new row

                // Add three cells to the row
                for (int c = 0; c < 3; c++)
                {
                    Cell cell = new Cell
                    {
                        // Simple text content
                        Paragraphs = { new TextFragment($"R{r + 1}C{c + 1}") },
                        // Optional cell padding
                        Margin = new MarginInfo(5, 5, 5, 5)
                    };
                    row.Cells.Add(cell);
                }
            }

            // Apply alternating background colors based on row index parity
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Row currentRow = table.Rows[i];
                // Even rows (0‑based) get LightGray, odd rows get White
                currentRow.BackgroundColor = (i % 2 == 0) ? Color.LightGray : Color.White;
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with alternating row colors: {outputPdf}");
    }
}
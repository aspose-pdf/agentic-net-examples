using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for Color

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with three columns of equal width
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // ----- Header row -----
            var header = table.Rows.Add();
            header.Cells.Add("Item");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // ----- Sample data rows -----
            var data = new object[,] {
                { "Apple",  10, 0.5 },
                { "Banana", 25, 0.3 },
                { "Cherry", 5,  1.2 }
            };

            // Add data rows to the table
            for (int r = 0; r < data.GetLength(0); r++)
            {
                var row = table.Rows.Add();
                row.Cells.Add(data[r, 0].ToString());               // Item
                row.Cells.Add(data[r, 1].ToString());               // Quantity
                row.Cells.Add(data[r, 2].ToString());               // Price
            }

            // ----- Conditional formatting -----
            // Define thresholds
            double quantityThreshold = 20;   // Highlight quantities > 20
            double priceThreshold    = 1.0; // Highlight prices > 1.0

            // Table.Rows and TableCell collections are 0‑based. Header row is at index 0.
            // Data rows therefore start at index 1.
            for (int r = 0; r < data.GetLength(0); r++)
            {
                // Row in the table that corresponds to the current data record
                var row = table.Rows[r + 1]; // +1 skips the header row (index 0)

                // Quantity is the second column (cell index 1)
                var qtyCell = row.Cells[1];
                double qty = Convert.ToDouble(data[r, 1]);
                if (qty > quantityThreshold)
                {
                    // LightGoldenrodYellow background for high quantities
                    qtyCell.BackgroundColor = Color.LightGoldenrodYellow;
                }

                // Price is the third column (cell index 2)
                var priceCell = row.Cells[2];
                double price = Convert.ToDouble(data[r, 2]);
                if (price > priceThreshold)
                {
                    // LemonChiffon background for high prices
                    priceCell.BackgroundColor = Color.LemonChiffon;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save("ConditionalFormatting.pdf");
        }
    }
}

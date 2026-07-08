using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ConditionalFormattingExample
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "ConditionalFormatting.pdf";

        // Create a DataTable with sample numeric data
        DataTable dt = new DataTable();
        dt.Columns.Add("Item", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("Price", typeof(double));

        dt.Rows.Add("Apple", 10, 0.5);
        dt.Rows.Add("Banana", 5, 0.3);
        dt.Rows.Add("Cherry", 20, 1.2);
        dt.Rows.Add("Date", 2, 2.5);
        dt.Rows.Add("Elderberry", 15, 0.8);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table and import the DataTable (first row will be a header)
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // three equal columns
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };
            table.ImportDataTable(dt, true, 0, 0);

            // Define thresholds
            const int quantityThreshold = 10; // highlight quantities > 10
            const double priceThreshold = 1.0;   // highlight prices > 1.0

            // Apply conditional formatting – skip the first (header) row
            bool firstRow = true;
            foreach (Row row in table.Rows)
            {
                if (firstRow)
                {
                    firstRow = false;
                    continue; // header row
                }

                // Quantity cell (second column, index 1)
                Cell qtyCell = row.Cells[1];
                // The first paragraph in a cell is a TextFragment after ImportDataTable
                string qtyText = ((TextFragment)qtyCell.Paragraphs[0]).Text;
                if (int.TryParse(qtyText, out int qty) && qty > quantityThreshold)
                {
                    qtyCell.BackgroundColor = Color.Yellow; // Aspose.Pdf.Color
                }

                // Price cell (third column, index 2)
                Cell priceCell = row.Cells[2];
                string priceText = ((TextFragment)priceCell.Paragraphs[0]).Text;
                if (double.TryParse(priceText, out double price) && price > priceThreshold)
                {
                    priceCell.BackgroundColor = Color.LightYellow; // Aspose.Pdf.Color
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

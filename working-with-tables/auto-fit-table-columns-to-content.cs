using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableAutoFit.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set ColumnAdjustment to AutoFitToContent
            Table table = new Table
            {
                // This makes the table automatically adjust column widths to fit the cell contents
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,

                // Optional: set table position and width
                Left = 50,
                Top = 700,
                // Width can be set; AutoFitToContent will still adjust columns within this width
                // If Width is not set, the table will expand as needed
                // Width = 500
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ----- Build table rows and cells -----
            // Header row
            Row headerRow = new Row();
            table.Rows.Add(headerRow);

            // Header cells
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Description"));
            headerRow.Cells.Add(headerCell2);

            Cell headerCell3 = new Cell();
            headerCell3.Paragraphs.Add(new TextFragment("Price"));
            headerRow.Cells.Add(headerCell3);

            // Data rows (example data)
            string[,] data = {
                { "Widget A", "A high‑quality widget with many features", "$19.99" },
                { "Gadget B", "Compact gadget, ideal for travel", "$9.50" },
                { "Tool C", "Durable tool for heavy‑duty tasks", "$29.75" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = new Row();
                table.Rows.Add(dataRow);

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell cell = new Cell();
                    // Use TextFragment to add text to the cell
                    cell.Paragraphs.Add(new TextFragment(data[i, j]));
                    dataRow.Cells.Add(cell);
                }
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
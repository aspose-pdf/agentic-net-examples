using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top) in points
                // (0,0) is bottom‑left corner of the page
                // Adjust as needed
                Left = 50,
                Top = 700,
                // Optional: set column widths (in points)
                ColumnWidths = "100 150 200"
            };

            // ----- Add first row -----
            Row headerRow = table.Rows.Add(); // creates a new Row and adds it to the table
            // Add cells to the header row
            headerRow.Cells.Add("Product");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Price");

            // Apply a bold style to header cells
            foreach (Cell cell in headerRow.Cells)
            {
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Color.Black
                };
                cell.BackgroundColor = Color.LightGray;
            }

            // ----- Add data rows -----
            // Example data
            string[,] data = {
                { "Widget A", "10", "$5.00" },
                { "Widget B", "7",  "$8.50" },
                { "Widget C", "15", "$3.20" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add(); // new row
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    // Add a cell with text content
                    dataRow.Cells.Add(data[i, j]);
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file
            const string outputPath = "TableExample.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
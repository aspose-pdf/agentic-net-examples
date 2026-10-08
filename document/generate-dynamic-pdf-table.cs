using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample data collection that determines the number of table rows
        var data = new List<(string Name, int Quantity)>
        {
            ("Apples", 10),
            ("Bananas", 5),
            ("Cherries", 12),
            ("Dates", 7)
        };

        const string outputPath = "dynamic_table.pdf";

        // Create the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns; set column widths and a border
            Table table = new Table
            {
                ColumnWidths = "200 100", // first column 200 units, second column 100 units
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Product");
            Cell headerCell2 = header.Cells.Add("Quantity");

            // Style header cells (background color, bold text, margins)
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
                cell.Margin = new MarginInfo(5, 5, 5, 5);
            }

            // Dynamically add a row for each item in the data collection
            foreach (var item in data)
            {
                Row row = table.Rows.Add();
                Cell nameCell = row.Cells.Add(item.Name);
                Cell qtyCell = row.Cells.Add(item.Quantity.ToString());

                // Optional styling for data cells
                foreach (Cell cell in row.Cells)
                {
                    cell.DefaultCellTextState = new TextState { FontSize = 11 };
                    cell.Margin = new MarginInfo(5, 5, 5, 5);
                }
            }

            // Add the completed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF file (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic table saved to '{outputPath}'.");
    }
}
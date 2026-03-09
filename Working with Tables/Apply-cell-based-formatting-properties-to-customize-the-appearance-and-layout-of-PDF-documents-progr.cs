using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is included as requested
using Aspose.Pdf.Text; // Needed for TextState

class Program
{
    static void Main()
    {
        const string outputPath = "FormattedTable.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table with 3 columns
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Set the column widths (percentage of page width)
                ColumnWidths = "33% 34% 33%",
                // Optional: set default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
            };

            // ---------- Header Row ----------
            Row headerRow = table.Rows.Add();
            // Header cell 1
            Cell headerCell1 = headerRow.Cells.Add("Product");
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell1.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
            headerCell1.VerticalAlignment = VerticalAlignment.Center;
            headerCell1.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            // Header cell 2
            Cell headerCell2 = headerRow.Cells.Add("Quantity");
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
            headerCell2.VerticalAlignment = VerticalAlignment.Center;
            headerCell2.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            // Header cell 3
            Cell headerCell3 = headerRow.Cells.Add("Price");
            headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell3.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
            headerCell3.VerticalAlignment = VerticalAlignment.Center;
            headerCell3.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // ---------- Data Rows ----------
            // Example data
            string[,] data = new string[,] {
                { "Widget A", "10", "$15.00" },
                { "Widget B", "5",  "$25.00" },
                { "Widget C", "20", "$8.00" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();

                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell dataCell = dataRow.Cells.Add(data[i, j]);

                    // Alternate row background color
                    if (i % 2 == 0)
                    {
                        // No background – simply do not set BackgroundColor
                    }
                    else
                    {
                        dataCell.BackgroundColor = Aspose.Pdf.Color.AliceBlue;
                    }

                    dataCell.DefaultCellTextState = new TextState { HorizontalAlignment = HorizontalAlignment.Center };
                    dataCell.VerticalAlignment = VerticalAlignment.Center;
                    dataCell.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.DarkGray);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with formatted table saved to '{outputPath}'.");
    }
}

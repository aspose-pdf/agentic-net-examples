using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the output PDF
        const string outputPath = "DynamicTable.pdf";

        // Simulated source records (could be read from a DB, CSV, etc.)
        List<string[]> sourceRecords = GetSourceRecords();

        // Determine the number of rows needed (excluding header)
        int rowCount = sourceRecords.Count;

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "200 200",
                // Optional: set border for visibility
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // ----- Header Row -----
            Row header = table.Rows.Add();
            // Header cells
            Cell headerCell1 = header.Cells.Add("Name");
            Cell headerCell2 = header.Cells.Add("Value");

            // Style header cells
            TextState headerStyle = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White,
                BackgroundColor = Color.Gray
            };
            headerCell1.DefaultCellTextState = headerStyle;
            headerCell2.DefaultCellTextState = headerStyle;

            // ----- Data Rows -----
            for (int i = 0; i < rowCount; i++)
            {
                // Each record is an array of strings: [column1, column2]
                string[] record = sourceRecords[i];

                Row dataRow = table.Rows.Add();
                Cell cell1 = dataRow.Cells.Add(record[0]);
                Cell cell2 = dataRow.Cells.Add(record[1]);

                // Optional: style data cells
                TextState dataStyle = new TextState
                {
                    FontSize = 10,
                    FontStyle = FontStyles.Regular,
                    ForegroundColor = Color.Black
                };
                cell1.DefaultCellTextState = dataStyle;
                cell2.DefaultCellTextState = dataStyle;
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic table saved to '{outputPath}'.");
    }

    // Mock method to retrieve source records.
    // Replace this with actual data retrieval logic as needed.
    static List<string[]> GetSourceRecords()
    {
        // Example records: each entry has two columns (Name, Value)
        return new List<string[]>
        {
            new[] { "Item A", "100" },
            new[] { "Item B", "200" },
            new[] { "Item C", "300" },
            // Add more records as required
        };
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AutoNumberedTableExample
{
    static void Main()
    {
        // Paths for input (if any) and output PDF
        const string outputPath = "AutoNumberedTable.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (left, top)
                Left = 50,
                Top = 700,
                // Define column widths (first column for numbers, others for data)
                ColumnWidths = "50 150 150"
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            header.Cells.Add("No.");          // Header for auto‑numbered column
            header.Cells.Add("First Name");
            header.Cells.Add("Last Name");

            // Sample data (could be loaded from elsewhere)
            string[,] data = new string[,]
            {
                { "John",  "Doe"   },
                { "Jane",  "Smith" },
                { "Alice", "Brown" },
                { "Bob",   "Johnson" }
            };

            // ----- Data rows with auto‑numbered first column -----
            for (int i = 0; i < data.GetLength(0); i++)
            {
                // Add a new row to the table
                Row row = table.Rows.Add();

                // First cell: sequential number (starting at 1)
                row.Cells.Add((i + 1).ToString());

                // Remaining cells: data from the array
                row.Cells.Add(data[i, 0]); // First Name
                row.Cells.Add(data[i, 1]); // Last Name
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑numbered table saved to '{outputPath}'.");
    }
}
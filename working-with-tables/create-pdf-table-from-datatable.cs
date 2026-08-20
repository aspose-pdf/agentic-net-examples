using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an in‑memory DataTable with sample data (no SqlClient required)
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        dataTable.Rows.Add(1, "Apple", 10);
        dataTable.Rows.Add(2, "Banana", 20);
        dataTable.Rows.Add(3, "Cherry", 30);

        const string outputPdfPath = "output.pdf";

        // Create a new PDF document and add a page
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            // Create a table and set equal column widths (integer percentages to avoid culture‑specific parsing issues)
            Table table = new Table();
            int columnCount = dataTable.Columns.Count;
            if (columnCount > 0)
            {
                // Use integer percentages – Aspose.Pdf parses the widths with the current culture, so we avoid decimal points.
                int baseWidth = 100 / columnCount;               // integer division
                int remainder = 100 % columnCount;               // distribute the leftover percentage
                List<string> widths = new List<string>();
                for (int i = 0; i < columnCount; i++)
                {
                    int width = baseWidth + (i == columnCount - 1 ? remainder : 0);
                    widths.Add(width.ToString(CultureInfo.InvariantCulture));
                }
                table.ColumnWidths = string.Join(",", widths);
            }

            // Import the DataTable into the PDF table (first row = column names)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the table to the page and save the document
            page.Paragraphs.Add(table);
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF table generated and saved to '{outputPdfPath}'.");
    }
}

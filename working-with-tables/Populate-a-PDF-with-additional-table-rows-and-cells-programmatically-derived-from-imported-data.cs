using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // existing PDF to populate
        const string outputPdfPath = "populated.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Sample data that will be imported into the table.
        DataTable data = new DataTable();
        data.Columns.Add("Product", typeof(string));
        data.Columns.Add("Quantity", typeof(int));
        data.Columns.Add("Price", typeof(decimal));

        data.Rows.Add("Widget A", 10, 9.99m);
        data.Rows.Add("Widget B", 5, 19.95m);
        data.Rows.Add("Widget C", 12, 4.50m);

        // Load the existing PDF, add a table, import the data, and save.
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure we work with the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Create a new table and set its position on the page.
            Table table = new Table
            {
                // Position the table 50 points from the left and 500 points from the bottom.
                Left = 50,
                Top  = 500,
                // Optional: set a border for visual clarity.
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Import the DataTable into the Aspose.Pdf.Table.
            // Parameters:
            //   data               – source DataTable
            //   true               – import column names as the first row
            //   0                  – start importing at the first row of the table (zero‑based)
            //   0                  – start importing at the first column of the table (zero‑based)
            table.ImportDataTable(data, true, 0, 0);

            // Add the populated table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with populated table saved to '{outputPdfPath}'.");
    }
}
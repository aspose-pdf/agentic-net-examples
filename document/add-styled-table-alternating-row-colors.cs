using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // existing PDF (can be empty)
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table
            Table table = new Table
            {
                // Define three equal columns
                ColumnWidths = "100 100 100",

                // Set a thin border around the whole table
                Border = new BorderInfo(BorderSide.All, 0.5f),

                // Default cell border and padding
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.LightGray; // distinct header background
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Sample data rows
            string[,] data = new string[,]
            {
                { "Widget A", "10", "$5.00" },
                { "Widget B", "7",  "$7.50" },
                { "Widget C", "3",  "$12.00" },
                { "Widget D", "15", "$3.20" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row row = table.Rows.Add();

                // Alternate row background colors
                if (i % 2 == 0)
                    row.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.95, 0.95, 0.95); // light gray

                // Add cells
                row.Cells.Add(data[i, 0]);
                row.Cells.Add(data[i, 1]);
                row.Cells.Add(data[i, 2]);
            }

            // Position the table on the page (optional)
            // Here we place it 50 points from the left and 700 points from the bottom
            table.Margin = new MarginInfo(50, 0, 0, 700);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}
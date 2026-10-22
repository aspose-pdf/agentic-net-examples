using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "conditional_formatted.pdf";
        const double threshold  = 50.0; // cells with value > 50 will be highlighted

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "100 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Item");
            header.Cells.Add("Value");

            // Sample data rows
            string[] items  = { "A", "B", "C", "D", "E" };
            double[] values = { 23.5, 67.2, 45.0, 89.1, 12.3 };

            for (int i = 0; i < items.Length; i++)
            {
                Row row = table.Rows.Add();
                // First cell: item name (text)
                row.Cells.Add(items[i]);

                // Second cell: numeric value (as string)
                string valueStr = values[i].ToString();
                Cell valueCell = row.Cells.Add(valueStr);

                // Conditional formatting: set background color if value exceeds threshold
                if (values[i] > threshold)
                {
                    // Use Aspose.Pdf.Color to avoid System.Drawing ambiguity
                    valueCell.BackgroundColor = Aspose.Pdf.Color.LightSalmon;
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with conditional formatting: {outputPath}");
    }
}
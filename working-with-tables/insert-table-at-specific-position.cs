using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for MarginInfo if needed (actually MarginInfo is in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Create a new table
            Table table = new Table();

            // Define column widths (optional)
            table.ColumnWidths = "100 150 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Widget A");
            data.Cells.Add("10");
            data.Cells.Add("$5.00");

            // Set the absolute position of the table on the page using MarginInfo
            // Left = X coordinate, Top = Y coordinate measured from the top of the page
            table.Margin = new MarginInfo { Left = 100, Top = 500 };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}

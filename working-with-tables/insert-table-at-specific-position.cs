using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // kept for possible future use

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new table. Position it using MarginInfo (coordinates are measured from the bottom‑left corner).
            Table table = new Table
            {
                ColumnWidths = "30% 70%"
            };

            // Desired absolute coordinates (e.g., 100 points from the left, 200 points from the top)
            float x = 100f;
            float yFromTop = 200f;
            // Convert page height (double) to float and calculate Y from bottom.
            float y = (float)page.PageInfo.Height - yFromTop;
            // Apply the margin to position the table.
            table.Margin = new MarginInfo(x, y, 0, 0);

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value A");
            data.Cells.Add("Value B");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}

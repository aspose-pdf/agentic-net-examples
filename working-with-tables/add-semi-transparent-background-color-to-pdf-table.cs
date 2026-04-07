using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (add a new one if needed)
            Page page = doc.Pages[1];

            // Create a table and set a semi‑transparent background color
            Table table = new Table
            {
                // Example: 50 % opaque blue (alpha 128 out of 255)
                BackgroundColor = Color.FromArgb(128, 0, 0, 255)
            };

            // Define column widths (two columns, each 200 points wide)
            table.ColumnWidths = "200 200";

            // First row (header)
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Second row (data)
            Row data = table.Rows.Add();
            data.Cells.Add("Value 1");
            data.Cells.Add("Value 2");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with background color saved to '{outputPath}'.");
    }
}

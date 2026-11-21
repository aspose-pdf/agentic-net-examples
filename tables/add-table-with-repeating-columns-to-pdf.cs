using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for text handling if needed

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and set RepeatingColumnsCount to repeat the first two columns
            Table table = new Table
            {
                RepeatingColumnsCount = 2,
                // Optional: define column widths (in points)
                ColumnWidths = "100 100 100"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add sample data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} Col 1");
                row.Cells.Add($"Row {i} Col 2");
                row.Cells.Add($"Row {i} Col 3");
            }

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with repeating columns saved to '{outputPath}'.");
    }
}
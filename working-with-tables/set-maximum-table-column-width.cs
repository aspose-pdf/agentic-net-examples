using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (wrapped in a using block for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based page indexing)
            Page page = doc.Pages[1];

            // Create a table and limit each column to a maximum width of 150 points
            Table table = new Table
            {
                // ColumnWidths is a space‑separated list of widths in points.
                // Here we define three columns, each capped at 150 points.
                ColumnWidths = "150 150 150"
            };

            // Optional: add a thin border to make column limits visible
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Column 1");
            header.Cells.Add("Column 2");
            header.Cells.Add("Column 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Data A");
            data.Cells.Add("Data B");
            data.Cells.Add("Data C");

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with limited column widths saved to '{outputPath}'.");
    }
}
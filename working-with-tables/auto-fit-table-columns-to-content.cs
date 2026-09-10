using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or add a new page if needed)
            Page page = doc.Pages[1];

            // Create a table and set it to auto‑fit its columns to the content
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Example: set a simple border for visual clarity
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Define column widths (optional; can be omitted for auto‑fit)
            table.ColumnWidths = "100 150 200";

            // Add a row with sample cells
            Row row = table.Rows.Add();
            row.Cells.Add("Short");
            row.Cells.Add("A much longer piece of text that should cause the column to auto‑fit");
            row.Cells.Add("Medium length");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with auto‑fit table to '{outputPath}'.");
    }
}
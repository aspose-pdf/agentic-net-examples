using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the source PDF (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table();

            // Set a maximum width (in points) for each column.
            // The value is a space‑separated list; each entry applies to the corresponding column.
            // Here we limit all columns to 120 points (≈1.67 inches).
            table.ColumnWidths = "120 120 120";

            // Optionally, also set a default column width that will be used
            // when the ColumnWidths string does not specify a value for a column.
            table.DefaultColumnWidth = "120";

            // Add a single row with sample cell contents.
            Row row = table.Rows.Add();
            row.Cells.Add("This is a long piece of text that should wrap within the column width.");
            row.Cells.Add("Short text");
            row.Cells.Add("Another cell");

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF (no SaveOptions needed for PDF output).
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with limited column widths saved to '{outputPath}'.");
    }
}
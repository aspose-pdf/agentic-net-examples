using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (or create a new one if you prefer)
        using (Document doc = new Document(inputPath))
        {
            // Add a new page to host the table (optional if you want to use an existing page)
            Page page = doc.Pages.Add();

            // Create a table and set a maximum column width.
            // The ColumnWidths property accepts a space‑separated list of widths.
            // Here we limit each of three columns to 120 points (≈1.67 inches).
            Table table = new Table
            {
                ColumnWidths = "120 120 120",          // max width for each column
                DefaultColumnWidth = "120",            // fallback width if ColumnWidths is incomplete
                RepeatingColumnsCount = 3              // ensure exactly three columns
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Long text that would normally expand the column beyond the limit, but will be wrapped.");
            data.Cells.Add("Short");
            data.Cells.Add("Another long piece of text that should be constrained.");

            // Enable word‑wrap for cells so content stays within the max width
            foreach (Row row in table.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    // Set basic text formatting
                    cell.DefaultCellTextState = new TextState
                    {
                        FontSize = 12
                    };
                    // Enable wrapping – the correct property is Cell.IsWordWrapped, not TextState.WordWrap
                    cell.IsWordWrapped = true;
                }
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with column width limits to '{outputPath}'.");
    }
}

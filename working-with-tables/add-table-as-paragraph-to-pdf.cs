using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a text fragment (BaseParagraph) and add it to the page
            // ------------------------------------------------------------
            TextFragment txtFragment = new TextFragment("Below is a sample table added as a paragraph:");
            // Position the fragment (X = 50, Y = 750). Y coordinate is measured from the bottom of the page.
            txtFragment.Position = new Position(50, 750);
            // Optional: set wrapping mode via TextState (Aspose.Pdf does word‑wrap by default for fragments)
            txtFragment.TextState.FontSize = 12;
            page.Paragraphs.Add(txtFragment);

            // ------------------------------------------------------------
            // 2. Create a Table (inherits from BaseParagraph)
            // ------------------------------------------------------------
            Table table = new Table();

            // Set table position and size (optional)
            table.Left = 50;   // X coordinate
            table.Top = 700;   // Y coordinate
            table.ColumnWidths = "200 200"; // two columns, each 200 units wide

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Price");

            // Add data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("$10.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("$15.00");

            // ------------------------------------------------------------
            // 3. Insert the Table as a paragraph after the text fragment
            // ------------------------------------------------------------
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // 4. Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}

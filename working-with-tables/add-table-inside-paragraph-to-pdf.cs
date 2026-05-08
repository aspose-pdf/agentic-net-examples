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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a text fragment (BaseParagraph) and add it to the page
            // ------------------------------------------------------------
            TextFragment txtFragment = new TextFragment("Below is a sample table added inside a paragraph:")
            {
                // Position the fragment on the page (coordinates are from the bottom‑left corner)
                Position = new Position(50, 750)
            };
            // Append the fragment to the page's paragraph collection
            page.Paragraphs.Add(txtFragment);

            // ------------------------------------------------------------
            // 2. Create a Table (also a BaseParagraph) and configure it
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Position the table below the text fragment
                // (Top coordinate is measured from the bottom of the page)
                Top  = 700,   // Y‑coordinate of the top edge
                Left = 50,    // X‑coordinate of the left edge
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // Add a few data rows
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("10");
            row1.Cells.Add("$15.00");

            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("5");
            row2.Cells.Add("$25.00");

            // ------------------------------------------------------------
            // 3. Insert the Table into the page's paragraph collection
            // ------------------------------------------------------------
            // The Table itself is a BaseParagraph, so we add it directly.
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Save the modified PDF (using the provided lifecycle rule)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}

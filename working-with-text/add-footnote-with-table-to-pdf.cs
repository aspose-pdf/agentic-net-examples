using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footnote.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will carry the footnote
            TextFragment tf = new TextFragment("This paragraph has a footnote with a table.");

            // Create the footnote (Note) object
            Note footnote = new Note();

            // Build a simple 2×2 table to place inside the footnote
            Table footTable = new Table();
            footTable.ColumnWidths = "100 100";

            // Header row
            footTable.Rows.Add(new Row());
            footTable.Rows[1].Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            footTable.Rows[1].Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });

            // Data row
            footTable.Rows.Add(new Row());
            footTable.Rows[2].Cells.Add(new Cell { Paragraphs = { new TextFragment("Value 1") } });
            footTable.Rows[2].Cells.Add(new Cell { Paragraphs = { new TextFragment("Value 2") } });

            // Add the table to the footnote's paragraph collection
            footnote.Paragraphs.Add(footTable);

            // Associate the footnote with the text fragment
            tf.FootNote = footnote;

            // Add the text fragment (with footnote) to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footnote table: '{outputPath}'.");
    }
}
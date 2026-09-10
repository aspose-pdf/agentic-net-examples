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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages[1];

            // Create a text fragment that will carry the footnote
            TextFragment textFragment = new TextFragment("Sample text with footnote.");

            // Create the footnote (Note) object
            Note footnote = new Note();

            // Build a simple table to place inside the footnote
            Table footTable = new Table();
            footTable.ColumnWidths = "100 100"; // two equal columns

            // First row (header)
            footTable.Rows.Add(new Row());
            footTable.Rows[1].Cells.Add("Header 1");
            footTable.Rows[1].Cells.Add("Header 2");

            // Second row (data)
            footTable.Rows.Add(new Row());
            footTable.Rows[2].Cells.Add("Value 1");
            footTable.Rows[2].Cells.Add("Value 2");

            // Add the table to the footnote's paragraph collection
            footnote.Paragraphs.Add(footTable);

            // Attach the footnote to the text fragment
            textFragment.FootNote = footnote;

            // Insert the text fragment (with footnote) into the page
            page.Paragraphs.Add(textFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnote table saved to '{outputPath}'.");
    }
}
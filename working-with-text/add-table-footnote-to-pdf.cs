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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment that will contain the footnote
            TextFragment tf = new TextFragment("This paragraph has a footnote.");

            // Create the footnote (Note) object
            Note footnote = new Note();

            // Create a table to be placed inside the footnote
            Table table = new Table
            {
                // Example column widths (two columns)
                ColumnWidths = "120 120"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Value 1");
            data.Cells.Add("Value 2");

            // Populate the footnote's Paragraphs collection with the table
            footnote.Paragraphs.Add(table);

            // Associate the footnote with the text fragment
            tf.FootNote = footnote;

            // Add the text fragment (with footnote) to the page
            page.Paragraphs.Add(tf);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with footnote table to '{outputPath}'.");
    }
}
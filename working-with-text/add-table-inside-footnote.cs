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

        // Load the source PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create a footnote (Note) that will hold the table
            Note footnote = new Note();

            // Build a simple table to place inside the footnote
            Table footTable = new Table();
            footTable.ColumnWidths = "100 200"; // two columns with specified widths

            // First row (header)
            footTable.Rows.Add(new Row());
            Row headerRow = footTable.Rows[footTable.Rows.Count];
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Second row (data)
            footTable.Rows.Add(new Row());
            Row dataRow = footTable.Rows[footTable.Rows.Count];
            dataRow.Cells.Add("Value 1");
            dataRow.Cells.Add("Value 2");

            // Add the table to the footnote's paragraph collection
            footnote.Paragraphs.Add(footTable);

            // Create a text fragment that references the footnote
            TextFragment tf = new TextFragment("This paragraph contains a footnote.");
            tf.FootNote = footnote;

            // Append the text fragment (with footnote) to the first page
            doc.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnote table saved to '{outputPath}'.");
    }
}
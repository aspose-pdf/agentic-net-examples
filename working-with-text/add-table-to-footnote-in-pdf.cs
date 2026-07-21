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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a text fragment that will contain the footnote
            TextFragment tf = new TextFragment("Sample text with footnote.");
            tf.Position = new Position(100, 700); // place the text on the page

            // Create a footnote (Note) and assign it to the TextFragment
            Note footNote = new Note();
            tf.FootNote = footNote;

            // Build a simple table to be placed inside the footnote
            Table footTable = new Table();
            footTable.ColumnWidths = "100 100"; // two columns, each 100 units wide
            footTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            footTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Header row
            Row header = footTable.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Data row 1
            Row row1 = footTable.Rows.Add();
            row1.Cells.Add("Row1 Col1");
            row1.Cells.Add("Row1 Col2");

            // Data row 2
            Row row2 = footTable.Rows.Add();
            row2.Cells.Add("Row2 Col1");
            row2.Cells.Add("Row2 Col2");

            // Add the table to the footnote's paragraph collection
            footNote.Paragraphs.Add(footTable);

            // Add the text fragment (with its footnote) to the page
            page.Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
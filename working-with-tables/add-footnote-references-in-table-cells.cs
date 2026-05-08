using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_footnotes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable tagged content and set basic properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with footnotes");

            // Work with the first page
            Page page = doc.Pages[1];

            // Create a simple table (2 columns)
            Table table = new Table
            {
                ColumnWidths = "200 200"
            };
            page.Paragraphs.Add(table);

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Item");
            header.Cells.Add("Description");

            // Data row with a footnote reference in the second cell
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Widget A"); // first cell

            // Second cell – contains superscript number and a footnote
            Cell footnoteCell = dataRow.Cells.Add();

            // TextFragment with superscript "1" (Unicode U+00B9)
            TextFragment tf = new TextFragment("Widget A description\u00B9");
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;

            // Create the footnote content
            Note footNote = new Note("This widget is a prototype used for testing.");

            // Associate the footnote with the TextFragment
            tf.FootNote = footNote;

            // Add the TextFragment to the cell
            footnoteCell.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with footnotes to '{outputPath}'.");
    }
}

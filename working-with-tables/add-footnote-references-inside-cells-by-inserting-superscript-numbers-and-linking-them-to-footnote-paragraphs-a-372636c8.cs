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
        const string inputPath  = "input.pdf";   // existing PDF to modify
        const string outputPath = "output_with_footnotes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a tagged content tree (required for footnotes)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Create a simple table on the first page
            Page page = doc.Pages[1];
            Table table = new Table
            {
                ColumnWidths = "200 200", // two columns, equal width
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // ----- Row 1 -----
            Row row1 = table.Rows.Add();
            // Cell 1: regular text
            Cell cell11 = row1.Cells.Add();
            TextFragment tf11 = new TextFragment("Item A");
            cell11.Paragraphs.Add(tf11);

            // Cell 2: text with superscript footnote reference
            Cell cell12 = row1.Cells.Add();
            // Use Unicode superscript 1 (U+00B9) for the reference number
            TextFragment tf12 = new TextFragment("Value\u00B9");
            // Create a footnote (Note) and attach it to the fragment
            tf12.FootNote = new Note("Footnote 1: Explanation of the value shown in the cell.");
            // Optionally make the superscript smaller
            tf12.TextState.FontSize = 12; // normal size
            // Reduce size for the superscript character
            // (Aspose.Pdf does not support Rise; using smaller font size for the whole fragment)
            cell12.Paragraphs.Add(tf12);

            // ----- Row 2 -----
            Row row2 = table.Rows.Add();
            // Cell 1: regular text
            Cell cell21 = row2.Cells.Add();
            TextFragment tf21 = new TextFragment("Item B");
            cell21.Paragraphs.Add(tf21);

            // Cell 2: another footnote reference (superscript 2)
            Cell cell22 = row2.Cells.Add();
            TextFragment tf22 = new TextFragment("Result\u00B2");
            tf22.FootNote = new Note("Footnote 2: Additional details about the result.");
            tf22.TextState.FontSize = 12;
            cell22.Paragraphs.Add(tf22);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnotes saved to '{outputPath}'.");
    }
}
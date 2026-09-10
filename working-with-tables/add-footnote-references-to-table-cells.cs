using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footnotes.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to add the table
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // two equal columns
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // ----- First row -----
            Row row1 = table.Rows.Add();

            // Cell 1: regular text
            Cell cell11 = row1.Cells.Add();
            cell11.Paragraphs.Add(new TextFragment("Item A"));

            // Cell 2: text with a footnote reference (superscript 1)
            Cell cell12 = row1.Cells.Add();
            // Unicode superscript 1 = \u00B9
            TextFragment tf12 = new TextFragment("Value\u00B9");
            // Attach a footnote to this fragment
            tf12.FootNote = new Note("This is the footnote for Value¹.");
            cell12.Paragraphs.Add(tf12);

            // ----- Second row -----
            Row row2 = table.Rows.Add();

            // Cell 1: regular text
            Cell cell21 = row2.Cells.Add();
            cell21.Paragraphs.Add(new TextFragment("Item B"));

            // Cell 2: text with a footnote reference (superscript 2)
            Cell cell22 = row2.Cells.Add();
            // Unicode superscript 2 = \u00B2
            TextFragment tf22 = new TextFragment("Amount\u00B2");
            tf22.FootNote = new Note("Explanation for Amount² goes here.");
            cell22.Paragraphs.Add(tf22);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with footnotes saved to '{outputPath}'.");
    }
}
using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string marker = "InsertTableHere";

        // Create a new PDF document and add a page with a marker paragraph.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a paragraph that contains the marker text.
            TextFragment markerParagraph = new TextFragment(marker);
            page.Paragraphs.Add(markerParagraph);

            // Locate the paragraph index that contains the marker.
            int paragraphIndex = FindParagraphIndex(page, marker);
            if (paragraphIndex < 0)
            {
                Console.WriteLine("Marker paragraph not found. No table inserted.");
                doc.Save(outputPath);
                return;
            }

            // Build a simple table with two columns.
            Table table = new Table();
            table.ColumnWidths = "100 100"; // two equal columns
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Insert the table after the located paragraph.
            page.Paragraphs.Insert(paragraphIndex + 1, table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }

    // Returns the zero‑based index of the first paragraph containing the given text, or -1 if not found.
    static int FindParagraphIndex(Page page, string searchText)
    {
        for (int i = 0; i < page.Paragraphs.Count; i++)
        {
            if (page.Paragraphs[i] is TextFragment tf && tf.Text.Contains(searchText))
                return i;
        }
        return -1;
    }
}

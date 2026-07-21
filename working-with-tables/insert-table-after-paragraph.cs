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
        const string markerText = "Insert table after this paragraph.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (adjust as needed)
            Page page = doc.Pages[1];
            int paragraphIndex = -1;

            // Locate the paragraph that contains the marker text
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                if (page.Paragraphs[i] is TextFragment tf && tf.Text.Contains(markerText))
                {
                    paragraphIndex = i;
                    break;
                }
            }

            if (paragraphIndex == -1)
            {
                Console.WriteLine("Target paragraph not found.");
            }
            else
            {
                // Build a simple 2x2 table
                Table table = new Table
                {
                    // Optional: set column widths (in points)
                    ColumnWidths = "120 120"
                };

                // Header row
                Row header = table.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");

                // Data row
                Row data = table.Rows.Add();
                data.Cells.Add("Cell 1");
                data.Cells.Add("Cell 2");

                // Insert the table immediately after the found paragraph
                page.Paragraphs.Insert(paragraphIndex + 1, table);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
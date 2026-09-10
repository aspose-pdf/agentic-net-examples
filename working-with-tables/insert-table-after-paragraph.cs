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
        const string searchText = "Insert table after this paragraph";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the paragraph is on the first page
            Page page = doc.Pages[1];

            // Locate the paragraph by its text content
            int paragraphIndex = -1;
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                if (page.Paragraphs[i] is TextFragment tf && tf.Text.Contains(searchText))
                {
                    paragraphIndex = i;
                    break;
                }
            }

            if (paragraphIndex == -1)
            {
                Console.Error.WriteLine("Target paragraph not found.");
                return;
            }

            // Create a simple table with 2 columns and 2 rows
            Table table = new Table();
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide

            // First row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");

            // Second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 1");
            row2.Cells.Add("Cell 2");

            // Insert the table immediately after the found paragraph
            page.Paragraphs.Insert(paragraphIndex + 1, table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
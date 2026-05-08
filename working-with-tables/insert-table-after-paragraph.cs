using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableAfterParagraph
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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the paragraph is on the first page; adjust as needed
            Page page = doc.Pages[1];

            // Locate the paragraph (TextFragment) that contains the target text
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
                doc.Save(outputPath); // Save unchanged document
                return;
            }

            // Create a new table
            Table table = new Table
            {
                // Optional visual settings
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (optional)
            table.ColumnWidths = "100 150";

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                Font = FontRepository.FindFont("Helvetica")
            };

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell B1");

            // Insert the table immediately after the located paragraph
            // Paragraphs.Insert expects the index where the new element will be placed
            page.Paragraphs.Insert(paragraphIndex + 1, table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}
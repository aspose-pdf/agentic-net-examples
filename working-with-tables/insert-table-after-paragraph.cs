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
        const string searchText = "Insert table after this paragraph";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Assume the paragraph is on the first page; adjust as needed
            Page page = doc.Pages[1];

            // Locate the paragraph by its text content
            int paragraphIndex = -1;
            for (int i = 0; i < page.Paragraphs.Count; i++)
            {
                // Paragraphs collection holds BaseParagraph objects; we are interested in TextFragment
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

            // Create a simple table (you can customize rows/columns as required)
            Table table = new Table
            {
                // Example: set a light gray background and a thin border
                BackgroundColor = Aspose.Pdf.Color.LightGray,
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // ColumnWidths is a string; specify widths separated by commas or spaces
                ColumnWidths = "200 200"
            };

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            header.Cells.Add(headerCell1);
            header.Cells.Add(headerCell2);
            header.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };

            // Add a data row
            Row data = table.Rows.Add();
            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Cell A1"));
            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Cell B1"));
            data.Cells.Add(dataCell1);
            data.Cells.Add(dataCell2);
            data.DefaultCellTextState = new TextState { FontSize = 10 };

            // Insert the table immediately after the located paragraph
            page.Paragraphs.Insert(paragraphIndex + 1, table);

            // Save the modified document (lifecycle rule: use Save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and document saved to '{outputPath}'.");
    }
}

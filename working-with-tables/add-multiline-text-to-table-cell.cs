using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the table on
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table (core API, not the TaggedContent API)
            Table table = new Table
            {
                // Optional: set table width to page width minus margins
                ColumnWidths = "100%",
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // ----- Table Header (optional) -----
            Row headerRow = table.Rows.Add();
            Cell headerCell = headerRow.Cells.Add();
            headerCell.Paragraphs.Add(new TextFragment("Description")
            {
                // Make header bold for visual distinction
                TextState = { FontStyle = FontStyles.Bold }
            });

            // ----- Table Body -----
            Row dataRow = table.Rows.Add();
            Cell dataCell = dataRow.Cells.Add();

            // Add multiline text using separate TextFragment objects.
            // A line‑break fragment is a TextFragment that contains only "\n".
            dataCell.Paragraphs.Add(new TextFragment("First line of text."));
            dataCell.Paragraphs.Add(new TextFragment("\n"));
            dataCell.Paragraphs.Add(new TextFragment("Second line of text."));
            dataCell.Paragraphs.Add(new TextFragment("\n"));
            dataCell.Paragraphs.Add(new TextFragment("Third line of text."));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

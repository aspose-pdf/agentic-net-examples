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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with two columns of equal width
            Table table = new Table
            {
                ColumnWidths = "200 200",
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // First cell: plain text
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Plain text"));

            // Second cell: HTML content using HtmlFragment
            Cell cell2 = row.Cells.Add();
            HtmlFragment htmlFragment = new HtmlFragment("<b>Bold HTML</b> <i>Italic</i>");
            cell2.Paragraphs.Add(htmlFragment);

            // Insert the table into the page's content
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

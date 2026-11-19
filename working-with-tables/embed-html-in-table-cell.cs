using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // HtmlFragment resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a table and add it to the first page
            Table table = new Table
            {
                ColumnWidths = "200 200",   // two columns, each 200 points wide
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };
            doc.Pages[1].Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row (first column)
            Cell cell = row.Cells.Add();

            // Embed HTML markup inside the cell by adding an HtmlFragment
            // The HtmlFragment parses the HTML string and renders it as PDF content
            HtmlFragment html = new HtmlFragment("<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u>");
            cell.Paragraphs.Add(html);

            // Optionally add a second cell with plain text for comparison
            Cell secondCell = row.Cells.Add();
            secondCell.Paragraphs.Add(new TextFragment("Plain text cell"));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML‑embedded cell saved to '{outputPath}'.");
    }
}